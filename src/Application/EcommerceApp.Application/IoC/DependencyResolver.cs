using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.IoC
{
    public class DependencyResolver:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //IoC --> Interface çağırdığım zaman bana onun concrete yapısını getirmesi gerektiği işlemi burada söylüyorum

            //örn: builder.RegisterType<BaseRepo>().As<IBaseRepo>().InstancePerLifeTimeScope();
            //program.cs tarafunda yapacağım eklemeleri burada yapılabilir
            //örn olarak automapper eklmesi burdan yapılabilir

            base.Load(builder);
        }
    }
}
