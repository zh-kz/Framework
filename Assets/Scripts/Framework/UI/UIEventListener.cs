using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework.UI
{    
    public delegate void PointerEventHandler(PointerEventData eventData);
    public delegate void BaseEventHandler(BaseEventData eventData);
    public delegate void AxisEventHandler(AxisEventData eventData);

    /// <summary>
    /// UI事件监听器：管理所有UGUI事件，提供事件参数类
    ///             附加到所有要交互的UI元素上
    ///             类似EventTrigger，但是EventTrigger有冗余
    /// </summary>
    public class UIEventListener : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
    {
        /// <summary>
        /// 指针进入
        /// </summary>
        public event PointerEventHandler PointerEnter;
        /// <summary>
        /// 指针离开
        /// </summary>
        public event PointerEventHandler PointerExit;
        /// <summary>
        /// 指针按下
        /// </summary>
        public event PointerEventHandler PointerDown;
        /// <summary>
        /// 指针抬起
        /// </summary>
        public event PointerEventHandler PointerUp;
        /// <summary>
        /// 指针单击
        /// </summary>
        public event PointerEventHandler PointerClick;   
        /// <summary>
        /// 拖动事件有效之前
        /// </summary>     
        public event PointerEventHandler InitializePotentialDrag;
        /// <summary>
        /// 拖动开始之前
        /// </summary>
        public event PointerEventHandler BeginDrag;
        /// <summary>
        /// 拖动期间
        /// </summary>
        public event PointerEventHandler Drag;
        /// <summary>
        /// 拖动结束时
        /// </summary>
        public event PointerEventHandler EndDrag;
        /// <summary>
        /// 拖放
        /// </summary>
        public event PointerEventHandler Drop;
        /// <summary>
        /// 滚动
        /// </summary>
        public event PointerEventHandler Scroll;
        /// <summary>
        /// 对象更新
        /// </summary>
        public event BaseEventHandler UpdateSelected;
        /// <summary>
        /// 选择
        /// </summary>
        public event BaseEventHandler Select;
        /// <summary>
        /// 选择新对象
        /// </summary>
        public event BaseEventHandler Deselect;
        /// <summary>
        /// 提交
        /// </summary>
        public event BaseEventHandler Submit;
        /// <summary>
        /// 取消
        /// </summary>
        public event BaseEventHandler Cancel;
        /// <summary>
        /// 移动
        /// </summary>
        public event AxisEventHandler Move;

        /// <summary>
        /// 获取对象上的事件监听器，找不到时自动添加
        /// </summary>
        /// <param name="tf">对象的变换组件</param>
        /// <returns></returns>
        public static UIEventListener GetListener(Transform tf)
        {
            UIEventListener uIEvent = tf.GetComponent<UIEventListener>();
            if(uIEvent == null)
            {
                uIEvent = tf.gameObject.AddComponent<UIEventListener>();
            }
            return uIEvent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(BeginDrag != null)
            {
                BeginDrag(eventData);
            }
        }

        public void OnCancel(BaseEventData eventData)
        {
            if(Cancel != null)
            {
                Cancel(eventData);
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if(Deselect != null)
            {
                Deselect(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(Drag != null)
            {
                Drag(eventData);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if(Drop != null)
            {
                Drop(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(EndDrag != null)
            {
                EndDrag(eventData);
            }
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if(InitializePotentialDrag != null)
            {
                InitializePotentialDrag(eventData);
            }
        }

        public void OnMove(AxisEventData eventData)
        {
            if(Move != null)
            {
                Move(eventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(PointerClick != null)
            {
                PointerClick(eventData);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(PointerDown != null)
            {
                PointerDown(eventData);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(PointerEnter != null)
            {
                PointerEnter(eventData);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(PointerExit != null)
            {
                PointerExit(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(PointerUp != null)
            {
                PointerUp(eventData);
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            if(Scroll != null)
            {
                Scroll(eventData);
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if(Select != null)
            {
                Select(eventData);
            }
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if(Submit != null)
            {
                Submit(eventData);
            }
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if(UpdateSelected != null)
            {
                UpdateSelected(eventData);
            }
        }
    }
}
