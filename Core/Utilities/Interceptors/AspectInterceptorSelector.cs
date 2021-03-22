using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{

    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            classAttributes.Add(new PerformanceAspect(10)); // Tüm metotlarda yapılan işlem süresi 10 saniyeden fazla sürerse uyar demektir.
            // PerformanceAspect yolu => Core.Aspects.Autofac.Performance
            
            
            //Eğer başka Aspectler eklemek istersek PerformanceAspect gibi ekleme yapabiliriz.


            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }

}
