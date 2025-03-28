using AutoServiceContainer;
using DataAccessLayer.EFTech.EFDataContexts;
using System;
using System.Collections.Generic;

namespace TestTools.AutoServiceConfigurationImplementation
{
    public class UnitSut<T>
        : AutoServiceCreator<T> where T : class
    {
        public UnitSut()
        {
            Sut = CreateService<T>(dataBase: DataBaseType.SqlLiteDataBase);
            Context = GetContext<EFDataContext>();
        }
        public UnitSut(Dictionary<Type, object> mockedObjects)
        {
            MockedObjects = mockedObjects;
            Sut = CreateService<T>(dataBase: DataBaseType.SqlLiteDataBase);
            Context = GetContext<EFDataContext>();
        }

        public T Sut { get; set; }
        public EFDataContext Context { get; set; }
    }
}