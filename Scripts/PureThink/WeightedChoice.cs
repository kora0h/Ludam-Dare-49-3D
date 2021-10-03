using UnityEngine;


namespace PureThink.Tools
{
    /// <summary>
    /// 随机返回一个拥有权重的索引 -> int
    /// 两个数组, 1, 所有的选项; 2, 选项对应的权重
    /// </summary>
    public static class WeightedChoice
    {
        /// <summary>
        /// int类型的权重选择
        /// </summary>
        /// <param name="numberChoice">选项数组</param>
        /// <param name="weightedChoice">权重数组</param>
        /// <returns>返回一个以int为权重类型的索引</returns>
        public static int GetRandom(int[] numberChoice, int[] weightedChoice)
        {
            //calculate total weight
            int totalWeigthSum = 0;
            for (int i = 0; i < numberChoice.Length; i++)
            {
                totalWeigthSum += weightedChoice[i];
            }
            //random out a int that is bigger than 0, less than total sum
            int rand = Random.Range(0, totalWeigthSum);

            //Compare random result to choice
            for (int i = 0; i < numberChoice.Length; i++)
            {
                if (rand <= weightedChoice[i])
                {
                    //This is the One
                    return numberChoice[i];
                }
                //Not this one ,so subtract the last weight
                rand -= weightedChoice[i];
            }

            //if everything above is done ,but no result comes out ,that means something is wrong .just give -1;
            Debug.LogError("WeightedChoice.GetRandom didn't give a correct result !!");
            return -1;
        }

        /// <summary>
        /// float类型的权重选择
        /// </summary>
        /// <param name="numberChoice">选项数组</param>
        /// <param name="weightedChoice">权重数组</param>
        /// <returns>返回一个以float为权重类型的索引</returns>
        public static int GetRandom(int[] numberChoice, float[] weightedChoice)
        {
            //calculate total weight
            float totalWeigthSum = 0;
            for (int i = 0; i < numberChoice.Length; i++)
            {
                totalWeigthSum += weightedChoice[i];
            }
            //random out a int that is bigger than 0, less than total sum
            float rand = Random.Range(0, totalWeigthSum);

            //Compare random result to choice
            for (int i = 0; i < numberChoice.Length; i++)
            {
                if (rand <= weightedChoice[i])
                {
                    //This is the One
                    return numberChoice[i];
                }
                //Not this one ,so subtract the last weight
                rand -= weightedChoice[i];
            }

            //if everything above is done ,but no result comes out ,that means something is wrong .just give -1;
            Debug.LogError("WeightedChoice.GetRandom didn't give a correct result !!");
            return -1;
        }
    }
}

