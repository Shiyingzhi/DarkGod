using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSever.User
{
    public class Vector3
    {
        public Vector3(float positionX, float positionY, float positionZ, float RotationX, float RotationY, float RotationZ)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.positionZ = positionZ;
            this.RotationX = RotationX;
            this.RotationY = RotationY;
            this.RotationZ = RotationZ;
        }
        public float positionX;
        public float positionY;
        public float positionZ;
        public float RotationX;
        public float RotationY;
        public float RotationZ;
    }
}
