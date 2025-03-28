using AutoServiceContainer;
using DataAccessLayer.EFTech.EFDataContexts;
using System;
using System.Collections.Generic;

namespace TestTools.AutoServiceConfigurationImplementation
{
    public class IntegrationSut<T>
        : AutoServiceCreator<T> where T : class
    {
        public IntegrationSut()
        {
            Sut = CreateService<T>(dataBase: DataBaseType.SqlServerDataBase);
            Context = GetContext<EFDataContext>();
        }
        public IntegrationSut(Dictionary<Type, object> mockedObjects)
        {
            MockedObjects = mockedObjects;
            Sut = CreateService<T>(dataBase: DataBaseType.SqlServerDataBase);
            Context = GetContext<EFDataContext>();
        }

        public T Sut { get; set; }
        public EFDataContext Context { get; set; }
    }
}