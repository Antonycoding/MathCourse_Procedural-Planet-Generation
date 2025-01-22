using UnityEngine;
using UnityEngine.UI;

public class SliderRotationController : MonoBehaviour
{
    public Slider rotationSlider; // 拖拽 Slider 到此字段
    public GameObject targetObject; // 需要旋转的物体

    void Start()
    {
        if (rotationSlider != null)
        {
            // 添加监听事件，每当 Slider 值变化时调用 RotateObject 方法
            rotationSlider.onValueChanged.AddListener(RotateObject);
        }
    }
    // 旋转物体的方法
    void RotateObject(float value)
    {
        if (targetObject != null)
        {
            // 将 Slider 的值映射到旋转角度
            targetObject.transform.rotation = Quaternion.Euler(0, value, 0);
        }
    }

    void OnDestroy()
    {
        // 移除监听器，避免内存泄漏
        if (rotationSlider != null)
        {
            rotationSlider.onValueChanged.RemoveListener(RotateObject);
        }
    }

    

}
