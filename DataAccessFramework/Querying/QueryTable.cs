using System.Collections.Generic;

namespace DataAccessFramework.Querying
{
	/// <summary>
	/// Represents a table used in a query.
	/// </summary>
	public class QueryTable : TableBase
	{
		private readonly string _tableName;
		private readonly List<FieldReference> _fields = new List<FieldReference>();

		/// <summary>
		/// Creates a new <c>QueryTable</c> instance.
		/// </summary>
		/// <param name="tableName"></param>
		public QueryTable(string tableName)
		{
			_tableName = tableName;
		}

		/// <summary>
		/// Gets the table name
		/// </summary>
		public override string TableName { get { return _tableName; } }

		internal override void BuildSql(BuildSqlContext sqlContext)
		{
			sqlContext.Builder.Append("[");
			sqlContext.Builder.Append(_tableName);
			sqlContext.Builder.Append("] ");
			sqlContext.Builder.Append(sqlContext.ResolveAlias(this));
		}

		/// <summary>
		/// Creates a string representation of data table.
		/// </summary>
		public override string ToString()
		{
			return _tableName;
		}

		/// <summary>
		/// Creatse a new <see cref="FieldReference"/> for a field on this table
		/// </summary>
		/// <param name="name">The name of the field</param>
		public FieldReference Field(string name)
		{
			var result = new FieldReference(this, name);
			_fields.Add(result);
			return result;
		}

		/// <summary>
		/// Gets a reference to all the fields in the table
		/// </summary>
		protected internal override IEnumerable<FieldReference> Fields
		{
			get { return _fields.AsReadOnly(); }
		}
	}
}