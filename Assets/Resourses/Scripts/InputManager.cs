using System.Collections;
using GameUtils;
using UnityEngine;


namespace Managers
{
    public class InputManager : SingletonObject<InputManager>
    {
        #region Properties

        public bool AxisMoving { get { return (Mathf.Abs(AxisHorizontal) + Mathf.Abs(AxisVertical)) > 0; } }

        public float AxisHorizontal { get { return Input.GetAxis("Horizontal"); } }
        public float AxisVertical { get { return Input.GetAxis("Vertical"); } }

        #endregion
    }
}
