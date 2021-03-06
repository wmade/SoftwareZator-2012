// *****************************************************************************
// 
//  � Veler Software 2012. All rights reserved.
//  The current code and the associated software are the proprietary 
//  information of Etienne Baudoux from Veler Software and are
//  supplied subject to licence terms.
// 
//  www.velersoftware.com
// *****************************************************************************


using System;

namespace Mono.Cecil.Cil {

	public enum DocumentType {
		Other,
		Text,
	}

	public enum DocumentHashAlgorithm {
		None,
		MD5,
		SHA1,
	}

	public enum DocumentLanguage {
		Other,
		C,
		Cpp,
		CSharp,
		Basic,
		Java,
		Cobol,
		Pascal,
		Cil,
		JScript,
		Smc,
		MCpp,
	}

	public enum DocumentLanguageVendor {
		Other,
		Microsoft,
	}

	public sealed class Document {

		string url;

		byte type;
		byte hash_algorithm;
		byte language;
		byte language_vendor;

		byte [] hash;

		public string Url {
			get { return url; }
			set { url = value; }
		}

		public DocumentType Type {
			get { return (DocumentType) type; }
			set { type = (byte) value; }
		}

		public DocumentHashAlgorithm HashAlgorithm {
			get { return (DocumentHashAlgorithm) hash_algorithm; }
			set { hash_algorithm = (byte) value; }
		}

		public DocumentLanguage Language {
			get { return (DocumentLanguage) language; }
			set { language = (byte) value; }
		}

		public DocumentLanguageVendor LanguageVendor {
			get { return (DocumentLanguageVendor) language_vendor; }
			set { language_vendor = (byte) value; }
		}

		public byte [] Hash {
			get { return hash; }
			set { hash = value; }
		}

		public Document (string url)
		{
			this.url = url;
			this.hash = Empty<byte>.Array;
		}
	}
}
