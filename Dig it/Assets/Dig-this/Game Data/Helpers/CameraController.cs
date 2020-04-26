using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public float smooth = 0.5f;
    public float offset;
    public Transform lowestBallPublic;

    Vector3 finishPos;
    bool finished = false;
    Vector3 velocity;
    List<Transform> balls = new List<Transform>();

    public Transform LowestBall
    {
        get
        {
            if (balls.Count < 2)
                return balls[0];

            Transform lowest = balls[1];
          
            foreach(Transform ball in balls)
            {
                if (ball == null)
                    balls.Remove(ball);
                if (ball.position.y < lowest.position.y)
                    lowest = ball;
            }

            return lowest;
        }
    }

    void LateUpdate()
    {
        lowestBallPublic = LowestBall;

        if (finished)
        {
            transform.position = Vector3.SmoothDamp(transform.position, finishPos, ref velocity, smooth);
        }
        else
        {
            if (LowestBall != null)
            {
                Vector3 newPos = new Vector3(0, LowestBall.position.y + offset, -10);
               
                if (Mathf.Abs(newPos.y - transform.position.y) > 1)
                    transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smooth);
            }
        }
    }

    public void AddBall(Transform ball)
    {
        balls.Add(ball);
    }

    public void RemoveBall(Transform ball)
    {
        balls.Remove(ball);
    }

    public void Finish(Vector3 finishPos)
    {
        if (finished)
            return;

        this.finishPos = finishPos;
        this.finishPos.z = -10;
        finished = true;
    }
}
