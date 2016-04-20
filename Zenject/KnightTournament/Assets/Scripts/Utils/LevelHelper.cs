using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Knights
{
    class LevelHelper
    {
        private CameraController mCameraLeft;
        private CameraController mCameraRight;

        public LevelHelper(List<CameraController> cameras)
        {
            Assert.That(cameras.Count == 2);
            foreach(CameraController camera in cameras)
            {
                if (camera.tag == Tags.LeftCamera)
                    mCameraLeft = camera;
                if (camera.tag == Tags.RightCamera)
                    mCameraRight = camera;
            }
        }

        public void MaximizeLeftCamera()
        {
            mCameraLeft.Maximize();
            mCameraRight.Camera.enabled = false;
        }


        public float ExtentHeight
        {
            get
            {
                return mCameraLeft.Camera.orthographicSize;
            }
        }

        public float ScreenHeight
        {
            get
            {
                return ExtentHeight * 2.0f;
            }
        }

        public float ExtentWidth
        {
            get
            {
                return mCameraLeft.Camera.aspect * mCameraLeft.Camera.orthographicSize;
            }
        }

        public float ScreenWidth
        {
            get
            {
                return ExtentWidth * 2.0f;
            }
        }

        public float DistanceBetweenCameras 
        {
            get 
            {
                return Vector2.Distance(mCameraLeft.transform.position, mCameraRight.transform.position);
            }
        }
    }
}
