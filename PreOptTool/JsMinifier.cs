using System;
using System.IO;
using System.Text;

namespace ResourceMerge.Core
{
	public class JsMinifier
	{
		const int EOF = -1;

		static StringReader sr;
		static StringWriter sw;
		static StringBuilder sb;
		static int theA;
		static int theB;
		static int theLookahead = EOF;
		
		public static string GetMinifiedCode(string regularJsCode)
		{
			string result = null;
			using (sr = new StringReader(regularJsCode))
			{
				sb = new StringBuilder();
				using (sw = new StringWriter(sb))
				{
					jsmin();
				}
			}
			sw.Flush();
			result = sb.ToString();
			return result;
		}

		private static void jsmin()
		{
			theA = '\n';
			action(3);
			while (theA != EOF)
			{
				switch (theA)
				{
					case ' ':
						{
							if (isAlphanum(theB))
							{
								action(1);
							}
							else
							{
								action(2);
							}
							break;
						}
					case '\n':
						{
							switch (theB)
							{
								case '{':
								case '[':
								case '(':
								case '+':
								case '-':
									{
										action(1);
										break;
									}
								case ' ':
									{
										action(3);
										break;
									}
								default:
									{
										if (isAlphanum(theB))
										{
											action(1);
										}
										else
										{
											action(2);
										}
										break;
									}
							}
							break;
						}
					default:
						{
							switch (theB)
							{
								case ' ':
									{
										if (isAlphanum(theA))
										{
											action(1);
											break;
										}
										action(3);
										break;
									}
								case '\n':
									{
										switch (theA)
										{
											case '}':
											case ']':
											case ')':
											case '+':
											case '-':
											case '"':
											case '\'':
												{
													action(1);
													break;
												}
											default:
												{
													if (isAlphanum(theA))
													{
														action(1);
													}
													else
													{
														action(3);
													}
													break;
												}
										}
										break;
									}
								default:
									{
										action(1);
										break;
									}
							}
							break;
						}
				}
			}
		}

		private static void action(int d)
		{
			if (d <= 1)
			{
				put(theA);
			}
			if (d <= 2)
			{
				theA = theB;
				if (theA == '\'' || theA == '"')
				{
					for (; ; )
					{
						put(theA);
						theA = get();
						if (theA == theB)
						{
							break;
						}
						if (theA <= '\n')
						{
							throw new Exception(string.Format("Error: JSMIN unterminated string literal: {0}\n", theA));
						}
						if (theA == '\\')
						{
							put(theA);
							theA = get();
						}
					}
				}
			}
			if (d <= 3)
			{
				theB = next();
				if (theB == '/' && (theA == '(' || theA == ',' || theA == '=' ||
									theA == '[' || theA == '!' || theA == ':' ||
									theA == '&' || theA == '|' || theA == '?' ||
									theA == '{' || theA == '}' || theA == ';' ||
									theA == '\n'))
				{
					put(theA);
					put(theB);
					for (; ; )
					{
						theA = get();
						if (theA == '/')
						{
							break;
						}
						else if (theA == '\\')
						{
							put(theA);
							theA = get();
						}
						else if (theA <= '\n')
						{
							throw new Exception(string.Format("Error: JSMIN unterminated Regular Expression literal : {0}.\n", theA));
						}
						put(theA);
					}
					theB = next();
				}
			}
		}

		private static int next()
		{
			int c = get();
			if (c == '/')
			{
				switch (peek())
				{
					case '/':
						{
							for (; ; )
							{
								c = get();
								if (c <= '\n')
								{
									return c;
								}
							}
						}
					case '*':
						{
							get();
							for (; ; )
							{
								switch (get())
								{
									case '*':
										{
											if (peek() == '/')
											{
												get();
												return ' ';
											}
											break;
										}
									case EOF:
										{
											throw new Exception("Error: JSMIN Unterminated comment.\n");
										}
								}
							}
						}
					default:
						{
							return c;
						}
				}
			}
			return c;
		}
	
		private static int peek()
		{
			theLookahead = get();
			return theLookahead;
		}

		private static int get()
		{
			int c = theLookahead;
			theLookahead = EOF;
			if (c == EOF)
			{
				c = sr.Read();
			}
			if (c >= ' ' || c == '\n' || c == EOF)
			{
				return c;
			}
			if (c == '\r')
			{
				return '\n';
			}
			return ' ';
		}
		private static void put(int c)
		{
            string s = c == '\n' ? Environment.NewLine : ((char)c).ToString();
            sw.Write(s);
		}

		private static bool isAlphanum(int c)
		{
			return ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') ||
				(c >= 'A' && c <= 'Z') || c == '_' || c == '$' || c == '\\' ||
				c > 126);
		}
	}
}