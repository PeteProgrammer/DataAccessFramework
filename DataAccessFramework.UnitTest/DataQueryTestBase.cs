﻿using System.Data;
using Moq;
using NUnit.Framework;

namespace DataAccessFramework.UnitTest
{
	public class DataQueryTestBase
	{
		private Mock<DataTool> _dataToolMock = new Mock<DataTool>();
		protected string ExecutedSql;
		protected IDataParameter[] ExecutedParameters;

		[SetUp]
		public void Setup()
		{
			_dataToolMock = new Mock<DataTool> { CallBase = true };
			_dataToolMock.Setup(x => x.ExecuteSqlReader(It.IsAny<string>(), It.IsAny<IDataParameter[]>()))
				.Callback((string sql, IDataParameter[] parameters) =>
				          	{
				          		ExecutedSql = sql;
				          		ExecutedParameters = parameters;
				          	});
			_dataToolMock.Setup(x => x.CreateIntParameter(It.IsAny<string>(), It.IsAny<int?>()))
				.Returns((string name, int? value) =>
				         	{
				         		var parameterMock = new Mock<IDataParameter>();
				         		parameterMock.Setup(y => y.ParameterName).Returns(name);
				         		parameterMock.Setup(y => y.Value).Returns(value);
				         		return parameterMock.Object;
				         	});
		}

		protected void Execute(DataQuery query)
		{
			_dataToolMock.Object.ExecuteQuery(query);
		}
	}
}