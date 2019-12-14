using UnityEngine;

public class Movement
{
    public void Lerping(int prev, int to, float deltaTime, float timer, GameObject parent, GameObject player, Vector3[] postions, bool canMove, float limit)
    {
        if (!canMove)
        {
            timer += deltaTime;

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
            player.transform.position = new Vector3(xLerp.x, player.transform.position.y, player.transform.position.z);

            if (timer >= limit)
            {
                player.transform.rotation = new Quaternion(player.transform.rotation.x, 0, 0, 1);
                timer = 0;
                canMove = true;
            }

        }
    }
}
