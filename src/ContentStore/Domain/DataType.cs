using System;

namespace ContentStore.Domain {

	public enum DataType {
		EncryptedString,
		String,
		Date,
		DateTime,
		Decimal,
		Id,
		Boolean,
		Time,
		Integer,
		Array,
		Object
	}
}