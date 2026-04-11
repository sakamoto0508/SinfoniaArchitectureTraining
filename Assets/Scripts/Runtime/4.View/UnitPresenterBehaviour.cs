using System;
using UnityEngine;

namespace View
{
    /// <summary>
    ///     UnitPresenter を内部で生成して保持し、View 側の呼び出しを委譲する MonoBehaviour ラッパー。
    ///     プレゼンテーション責務を持つ View 層に配置することで、Adaptor を純粋クラスのまま保てます。
    /// </summary>
    public sealed class UnitPresenterBehaviour : MonoBehaviour
    {
        private object _presenter;
        private object _entity;
        private object _moveService;

        /// <summary> 指定した Entity と移動サービスをバインドし、内部の Presenter を初期化する。 </summary>
        /// <param name="entity">バインドする CharacterEntity（null を許容）。</param>
        /// <param name="moveService">移動サービス（Application.IMoveService）。null 可。</param>
        public void Initialize(object entity, object moveService)
        {
            _entity = entity;
            _moveService = moveService;

            // 内部 Presenter を遅延生成する（リフレクションで生成して Adaptor へのコンパイル時依存を排除する）。
            if (_presenter == null && moveService != null)
            {
                try
                {
                    // アセンブリ内から Adaptor.UnitPresenter 型を探索して生成する
                    Type presenterType = null;
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        var t = asm.GetType("Adaptor.UnitPresenter");
                        if (t != null)
                        {
                            presenterType = t;
                            break;
                        }
                    }

                    if (presenterType != null)
                    {
                        _presenter = Activator.CreateInstance(presenterType, new object[] { moveService });
                    }
                }
                catch
                {
                    _presenter = null;
                }
            }

            if (_presenter != null && _entity != null)
            {
                try
                {
                    // Try to find DTO-aware Bind overload first
                    var bindDto = _presenter.GetType().GetMethod("Bind", new Type[] { typeof(Adaptor.PresenterDto) });
                    if (bindDto != null)
                    {
                        // Build PresenterDto from available data if possible
                        try
                        {
                            var unitIdProp = _entity.GetType().GetProperty("UnitId");
                            var templateProp = _entity.GetType().GetProperty("TemplateId");
                            var uid = unitIdProp != null ? (Guid)unitIdProp.GetValue(_entity) : Guid.Empty;
                            var templateId = templateProp != null ? (string)templateProp.GetValue(_entity) : string.Empty;
                            var displayName = string.Empty;
                            var pos = UnityEngine.Vector3.zero;
                            var dto = new Adaptor.PresenterDto(uid, templateId, displayName, pos);
                            bindDto.Invoke(_presenter, new object[] { dto });
                        }
                        catch { }
                    }
                    else
                    {
                        var bind = _presenter.GetType().GetMethod("Bind");
                        bind?.Invoke(_presenter, new object[] { _entity });
                    }
                }
                catch { }
            }
        }

        /// <summary> バインドされた CharacterEntity を返す。 </summary>
        public object Entity
        {
            get
            {
                if (_presenter != null)
                {
                    try
                    {
                        var prop = _presenter.GetType().GetProperty("Entity");
                        if (prop != null)
                        {
                            var val = prop.GetValue(_presenter);
                            if (val != null) return val;
                        }
                    }
                    catch { }
                }

                return _entity;
            }
        }

        /// <summary> Presenter が提供する表示名を返す。Presenter が存在しない場合は空文字を返す。 </summary>
        public string GetDisplayName()
        {
            if (_presenter == null) return string.Empty;
            try
            {
                var m = _presenter.GetType().GetMethod("GetDisplayName");
                if (m != null)
                {
                    var res = m.Invoke(_presenter, Array.Empty<object>());
                    if (res is string s) return s;
                }

                // If presenter exposes PresenterDto, use it
                var getDto = _presenter.GetType().GetMethod("ToPresenterDto");
                if (getDto != null)
                {
                    var dtoObj = getDto.Invoke(_presenter, Array.Empty<object>());
                    if (dtoObj is Adaptor.PresenterDto dto) return dto.DisplayName;
                }
            }
            catch { }
            return string.Empty;
        }

        /// <summary> 指定 ID の位置情報を取得する。MoveService が利用できない場合は (0,0,0) を返す。 </summary>
        public Vector3 GetPosition(Guid id)
        {
            try
            {
                if (_moveService == null) return Vector3.zero;
                var m = _moveService.GetType().GetMethod("GetPosition");
                if (m == null) return Vector3.zero;
                var res = m.Invoke(_moveService, new object[] { id });
                if (res is Vector3 v) return v;
                return Vector3.zero;
            }
            catch { }
            return Vector3.zero;
        }
    }
}
