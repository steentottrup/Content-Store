using System;

namespace ContentStore.Domain {

	public static class DataTypeExtensions {

		public static Boolean IsSimpleDataType(this DataType dataType) {
			return dataType == DataType.Boolean || dataType == DataType.DateTime || dataType == DataType.Id || dataType == DataType.Integer || dataType == DataType.Integer || dataType == DataType.EncryptedString;
		}

		public static Boolean IsComplexDataType(this DataType dataType) {
			return dataType == DataType.Array || dataType == DataType.Object;
		}
	}
}