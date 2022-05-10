using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Platformer
{
    public class RopeGeneratorController
    {
        private LineRenderer lineRenderer;
        private List<RopeSegment> ropeSegments;
        private float _ropeSegLen;
        private int _segmentLength;
        private float _lineWidth;
        private Vector2 _StartPoint;
        private Vector2 _EndPoint;

        public RopeGeneratorController(HookView view, int segmentLength, float lineWidth,
            float ropeSegLen)
        {
            _ropeSegLen = ropeSegLen;
            _lineWidth = lineWidth;
            lineRenderer = view.lineRenderer;
            _segmentLength = segmentLength;
            ropeSegments = new List<RopeSegment>();

            lineRenderer.enabled = false;

            Vector3 ropeStartPoint = _StartPoint;
        }

        public void GeneratePoints()
        {
            for (int i = 0; i < _segmentLength; i++)
            {
                ropeSegments.Add(new RopeSegment(_StartPoint));
                _StartPoint.y -= _ropeSegLen;
            }
        }

        public void ActiveRope(bool active)
        {
            lineRenderer.enabled = active;
        }

        public void UpdateStart(Vector2 startPoint)
        {
            _StartPoint = startPoint;
        }

        public void UpdateEnd(Vector2 endPoint)
        {
            _EndPoint = endPoint;
        }

        public void UpdateSeg(int length)
        {
            _segmentLength = length;
        }

        public void Simulate()
        {
            // SIMULATION
            Vector2 forceGravity = new Vector2(0f, -1f);

            for (int i = 1; i < this._segmentLength; i++)
            {
                RopeSegment firstSegment = this.ropeSegments[i];
                Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
                firstSegment.posOld = firstSegment.posNow;
                firstSegment.posNow += velocity;
                firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
                this.ropeSegments[i] = firstSegment;
            }

            //CONSTRAINTS
            for (int i = 0; i < 50; i++)
            {
                this.ApplyConstraint();
            }
        }

        private void ApplyConstraint()
        {
            //Constrant to First Point 
            RopeSegment firstSegment = this.ropeSegments[0];
            firstSegment.posNow = this._StartPoint;
            this.ropeSegments[0] = firstSegment;


            //Constrant to Second Point 
            RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
            endSegment.posNow = this._EndPoint;
            this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

            for (int i = 0; i < this._segmentLength - 1; i++)
            {
                RopeSegment firstSeg = this.ropeSegments[i];
                RopeSegment secondSeg = this.ropeSegments[i + 1];

                float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
                float error = Mathf.Abs(dist - this._ropeSegLen);
                Vector2 changeDir = Vector2.zero;

                if (dist > _ropeSegLen)
                {
                    changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
                }
                else if (dist < _ropeSegLen)
                {
                    changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
                }

                Vector2 changeAmount = changeDir * error;
                if (i != 0)
                {
                    firstSeg.posNow -= changeAmount * 0.5f;
                    this.ropeSegments[i] = firstSeg;
                    secondSeg.posNow += changeAmount * 0.5f;
                    this.ropeSegments[i + 1] = secondSeg;
                }
                else
                {
                    secondSeg.posNow += changeAmount;
                    this.ropeSegments[i + 1] = secondSeg;
                }
            }
        }

        public void DrawRope()
        {
            float lineWidth = this._lineWidth;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            Vector3[] ropePositions = new Vector3[this._segmentLength];
            for (int i = 0; i < this._segmentLength; i++)
            {
                ropePositions[i] = this.ropeSegments[i].posNow;
            }

            lineRenderer.positionCount = ropePositions.Length;
            lineRenderer.SetPositions(ropePositions);
        }
    }

    public struct RopeSegment
    {
        public Vector2 posNow; 
        public Vector2 posOld;
        public RopeSegment(Vector2 pos) 
        { 
            posNow = pos; 
            posOld = pos; 
        }
    }
}