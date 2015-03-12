using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;

namespace Ray.Plat.Common
{
	/// <summary>
	/// ������صĲ�����
	/// </summary>
	public class DataCache
	{
        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��key��Cacheֵ����ֵΪnull����ֵΪvalue
        /// </summary>
        /// <returns></returns>
        public static object GetCache(string key, object value)
        {
            object objModel = GetCache(key);
            if (objModel == null)
            {
                try
                {
                    objModel = value;
                    if (objModel != null)
                    {
                        int ModelCache = Plat.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Plat.Common.DataCache.SetCache(key, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return objModel;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<T> GetCache<T>(string key)
        {
            IList<T> results = (IList<T>)GetCache(key);
            return results;
        }

        /// <summary>
        /// �趨����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCache<T>(string key, IList<T> value)
        {
            Plat.Common.DataCache.SetCache(key, value, DateTime.Now.AddHours(2), TimeSpan.Zero);
        }

		/// <summary>
		/// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <returns></returns>
		public static object GetCache(string CacheKey)
		{
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
		}

        /// <summary>
        /// �Ƴ�����
        /// </summary>
        /// <param name="CacheKey"></param>
        public static void SetCacheOutDate(string CacheKey)
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }

        //������л���
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                _cache.Remove(key);
            }
        }

		/// <summary>
		/// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject)
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(CacheKey, objObject);
		}

		/// <summary>
		/// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration,TimeSpan slidingExpiration )
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(CacheKey, objObject,null,absoluteExpiration,slidingExpiration);
		}
	}
}

