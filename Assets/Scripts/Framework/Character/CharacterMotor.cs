using UnityEngine;
using UnityEngine.AI;

namespace Framework.Character
{
    /// <summary>
    /// 角色运动
    /// </summary>
    public class CharacterMotor : CachedBehaviour
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed = 5;

        /// <summary>
        /// 转向速度
        /// </summary>
        public float rotationSpeed = 0.5f;

        private NavMeshAgent agent;
        private CharacterAnimation characterAnimation;

        private float curSpeed;

        private bool isMoving;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            characterAnimation = GetComponent<CharacterAnimation>();
        }

        private void Update()
        {
            if(isMoving)
            {
                curSpeed = agent.velocity.sqrMagnitude;
                characterAnimation.SetFloat(CharacterAnimation.hashSpeed, curSpeed);
            }
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="dest">目标位置</param>
        /// <param name="stopDis">停止距离</param>
        public void MoveTo(Vector3 dest, float stopDis = 0)
        {
            agent.isStopped = false;
            agent.stoppingDistance = stopDis;
            agent.destination = dest;
            
            isMoving = true;
        }

    }
}
