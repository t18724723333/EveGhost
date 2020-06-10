using System;

namespace MODEL
{
    [Serializable]
    public static class Config
    {

        /// <summary>
        /// 角色数量
        /// </summary>
        public static int RoleNumber { get; set; }



        public static void GetConfig(TempConfig tc)
        {
            RoleNumber = tc.RoleNumberTemp;
        }
        public static TempConfig GetTempConfig()
        {
            TempConfig tc = new TempConfig
            {
                RoleNumberTemp = RoleNumber
            };

            return tc;
        }
    }
    [Serializable]
    public class TempConfig
    {
        public int RoleNumberTemp;


    }

}
