using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScripts : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToDisable;

    private bool knockedDown = false;
    // 각 컴포넌트의 활성화 상태를 저장하기 위한 구조체
    private struct ComponentState
    {
        public bool animatorEnabled;
        public bool rigidbodyConstraintsEnabled;
        public bool rigidbodyIsKinematic;
        public bool rigidbodyUseGravity;
        public bool copyMotionEnabled;
        public bool tpsPlayerControllerEnabled;
    }

    // 원래 컴포넌트 상태를 저장하는 딕셔너리
    private Dictionary<GameObject, ComponentState> originalStates = new Dictionary<GameObject, ComponentState>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) || knockedDown)
        {
            DisableState();
        }
    }
    private void Awake()
    {
       
    }
    void Start()
    {
        // 초기 상태 저장
        foreach (var obj in objectsToDisable)
        {
            ComponentState state = new ComponentState();

            var animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                state.animatorEnabled = animator.enabled;
            }

            var rigidbody = obj.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                // RigidbodyConstraints.None이 아니라면 제약 조건이 활성화된 것으로 간주
                state.rigidbodyConstraintsEnabled = rigidbody.constraints != RigidbodyConstraints.None;
                rigidbody.isKinematic = false; 
            }

            var copyMotion = obj.GetComponent<copymotion>();
            if (copyMotion != null)
            {
                state.copyMotionEnabled = copyMotion.enabled;
            }

            var tpsPlayerController = obj.GetComponent<TPSPlayerController>();
            if (tpsPlayerController != null)
            {
                state.tpsPlayerControllerEnabled = tpsPlayerController.enabled;
            }

            originalStates[obj] = state;
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "KnockDownCube")
        {
            DisableState();
        }
    }
    private void DisableState()
    {
        foreach (var obj in objectsToDisable)
        {
            var animator = obj.GetComponent<Animator>();
            if (animator != null && animator.enabled)
            {
                animator.enabled = false;
            }

            var rigidbody = obj.GetComponent<Rigidbody>();
            if (rigidbody != null && rigidbody.constraints != RigidbodyConstraints.None)
            {
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.isKinematic = false; // Kinematic을 비활성화
                rigidbody.useGravity = true;   // UseGravity를 활성화
            }

            var copyMotion = obj.GetComponent<copymotion>();
            if (copyMotion != null && copyMotion.enabled)
            {
                copyMotion.enabled = false;
            }
            var tpsPlayerController = obj.GetComponent<TPSPlayerController>();
            if (tpsPlayerController != null)
            {
                tpsPlayerController.enabled = false; // TPSPlayerController 비활성화
            }
        }
       
        StartCoroutine(StunDuration(3f));
    }
    private IEnumerator StunDuration(float duration)
    {
        yield return new WaitForSeconds(3);


        // 3초 후에 키 입력을 기다림
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W) ||
                                     Input.GetKeyDown(KeyCode.A) ||
                                     Input.GetKeyDown(KeyCode.S) ||
                                     Input.GetKeyDown(KeyCode.D));

        StandUpState(); // WASD 키 중 하나가 눌리면 일어남
        
    }
    private IEnumerator ReactivateAfterDelay(float delay) // 쳐맞고 다시 일나는 시간
    {
      
        yield return new WaitForSeconds(3);

        
        StandUpState();
    }
    private void StandUpState()
    {
        foreach (var obj in objectsToDisable)
        {
            var state = originalStates[obj];

            var animator = obj.GetComponent<Animator>();
            if (animator != null && state.animatorEnabled)
            {
                animator.enabled = true;
            }

            var rigidbody = obj.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.constraints = state.rigidbodyConstraintsEnabled ?
                                        RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition :
                                        RigidbodyConstraints.None;

                rigidbody.isKinematic = state.rigidbodyIsKinematic;
                rigidbody.useGravity = state.rigidbodyUseGravity;
                rigidbody.useGravity = true;   // UseGravity를 활성화
            }

            var copyMotion = obj.GetComponent<copymotion>();
            if (copyMotion != null && state.copyMotionEnabled)
            {
                copyMotion.enabled = true;
            }
            var tpsPlayerController = obj.GetComponent<TPSPlayerController>();
            if (tpsPlayerController != null && state.tpsPlayerControllerEnabled)
            {
                tpsPlayerController.enabled = true; // TPSPlayerController 활성화
            }
        }
    }
}
