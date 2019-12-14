using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class PlayerController : MonoBehaviour
    {
        public GameControllerScript gameController;
        public GameObject parent;
        public Rigidbody rb;
        public MeshRenderer mesh;

        bool isHit = false;

        public AudioSource pickUpSound;
        public AudioSource crashSound;
        public BoxCollider boxColl;

        public float timer = 0f;
        float hitTimer = 0f;
        float blinkTimer = 0f;
        float limit = 1f;

        bool canMove = true;
        bool canJump = true;

        public Vector3[] postions = new Vector3[3];
        public int currentPos = 1;
        public int prevPos;

        int score = 0;

        public IUnityService UnityService;

        // Start is called before the first frame update
        void Start()
        {

            if (UnityService == null)
            {
                UnityService = new UnityService();
            }

            postions[0] = new Vector3(-2.5f, transform.position.y, transform.position.z);
            postions[1] = new Vector3(0f, transform.position.y, transform.position.z);
            postions[2] = new Vector3(2.5f, transform.position.y, transform.position.z);
        }

        // Update is called once per frame
        public void Update()
        {

            bool inputA = UnityService.GetKeyDown(KeyCode.A);
            bool inputD = UnityService.GetKeyDown(KeyCode.D);
            bool inputSpace = UnityService.GetKeyDown(KeyCode.Space);

            LerpingPlayer(prevPos, currentPos, UnityService.GetDeltaTime());

            MovePlayer(inputA, inputD, inputSpace);

            HitTimer();

            transform.Rotate(new Vector3(10, transform.rotation.y, transform.rotation.z));

            if (transform.position.y < 0.4f)
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);
                canJump = true;
            }
        }

        public void MovePlayer(bool keyA, bool keyD, bool inputSpace)
        {
            if (keyA)
            {
                if (currentPos > 0 && canMove)
                {
                    prevPos = currentPos;
                    currentPos--;
                    canMove = false;
                }
            }

            if (keyD)
            {
                if (currentPos < 2 && canMove)
                {
                    prevPos = currentPos;
                    currentPos++;
                    canMove = false;
                }
            }

            if (inputSpace && canJump)
            {
                Jump();
                canJump = false;

            }
        }

        public void LerpingPlayer(int prev, int to, float deltaTime)
        {
            if (!canMove)
            {
                timer += deltaTime * 3f;

                var direction = prev - to;

                if (direction < 0)
                {
                    parent.transform.Rotate(new Vector3(0, 1, 0));
                }
                else
                {
                    parent.transform.Rotate(new Vector3(0, -1, 0));
                }

                Vector3 xLerp = Vector3.Lerp(postions[prev], postions[to], timer);
                transform.position = new Vector3(xLerp.x, transform.position.y, transform.position.z);

                if (timer >= limit)
                {
                    transform.rotation = new Quaternion(transform.rotation.x, 0, 0, 1);
                    timer = 0;
                    canMove = true;
                }

            }
        }

        public void Jump()
        {
            if (canJump)
            {
                rb.AddForce(new Vector3(0f, 300f, 0f));
                rb.useGravity = true;
            }
        }

        public void HitTimer()
        {
            if (isHit)
            {
                mesh.enabled = false;
                Blinking();

                hitTimer += UnityService.GetDeltaTime();

                if (hitTimer >= 1f)
                {
                    hitTimer = 0;
                    boxColl.enabled = true;
                    mesh.enabled = true;
                    isHit = false;
                }
            }
        }

        public void Blinking()
        {
            blinkTimer += UnityService.GetDeltaTime();

            if (blinkTimer >= 0.125)
            {
                mesh.enabled = true;
                blinkTimer = 0f;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "pickUp")
            {
                pickUpSound.Play();
                score += 10;
                gameController.totalScore.text = score.ToString();
                other.gameObject.SetActive(false);
            }

            if (other.tag == "Obstacle")
            {
                crashSound.Play();
                gameController.lives--;
                other.gameObject.SetActive(false);
                boxColl.enabled = false;
                isHit = true;
            }

            if (other.gameObject.tag == "EndGame")
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

