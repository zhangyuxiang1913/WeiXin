using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeiXinModel.model;

namespace WeiXinBLL
{
    public static class dbContext
    {
        //  string conn = System.Configuration.ConfigurationManager.ConnectionStrings[""].ConnectionString;
        private static testEntities db = DbContextFactory.GetCurrentDbContext();
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int AddEntity<T>(T entity) where T : class
        {

            db.Entry<T>(entity).State = EntityState.Added;
            return db.SaveChanges();
            // DbContextFactory db = new DbContextFactory();
            //  db.
            //  db = new DbContextFactory.GetCurrentDbContext();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static bool UpdateEntity<T>(T entity, params string[] properties) where T : class
        {
            var ent = db.Entry(entity);
            db.Set<T>().Attach(entity);
            if (properties.Length > 0)
            {
                foreach (var pro in properties)
                {
                    ent.Property(pro).IsModified = true;
                }
            }
            else
            {
                db.Entry<T>(entity).State = EntityState.Modified;
            }
            return db.SaveChanges() > 0;
        }
        public static bool DeleteEntity<T>(T entity) where T : class
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
        //简单查询
        public static IQueryable<T> LoadEntities<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
           return db.Set<T>().Where<T>(whereLambda).AsQueryable();
        }
        /// <summary>
        /// 新增修改实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static bool SaveEntity<T>(T entity, params string[] properties) where T : class
        {
            var ent = db.Entry(entity);

            if (ent.State == EntityState.Detached)//新增
            {
                ent.State = System.Data.Entity.EntityState.Added;

            }
            else//修改
            {


                db.Set<T>().Attach(entity);

                if (properties.Length > 0)
                {
                    foreach (var prop in properties)
                    {
                        ent.Property(prop).IsModified = true;
                    }
                }
                else
                {
                    ent.State = System.Data.Entity.EntityState.Modified;
                }

            }
            return db.SaveChanges() > 0;
        }
        //public static int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        //{
        //    return db.Set<T>().Where<T>(predicate).Delete();
        //}
    }
}


