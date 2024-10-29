// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("sPeLWybJDssIIbO6t6XLSiYA0DuPDAINPY8MBw+PDAwNwgpqC8gVIbbiFsse1Je1olEHGPJZJC1yk4C8zj/jh/B3BUT/zRWJ1cDGh9xCs8ZRG7iAcrPYw0iGd1EC/fX17skS0o5by2M54lCbZOOKGXFNk6RqYqnEoTCtsP6EMNihaoDHjPRpF9sJvnsyc98i7jEFXvNumN+5AHguL9c4N4nVFc91CuFvHE1wD7YnDrCnKac5Hn6iE06XHL7AQCcts0YjPFvBgDI9jwwvPQALBCeLRYv6AAwMDAgNDiOmrSdITMQi74B1+xK94G7xdF+LD+cXJpQXUmajJhfYHM5aSBbSOMln4TKmnJa1Kp+42S9SeHKYY+oSCj7eV2vS04HLhg8ODA0M");
        private static int[] order = new int[] { 9,1,13,12,10,8,11,7,11,11,11,12,13,13,14 };
        private static int key = 13;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
