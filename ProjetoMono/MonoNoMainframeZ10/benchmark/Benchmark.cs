/* original code copyright 2004 Christopher W. Cowell-Shah http://www.cowell-shah.com/research/benchmark/code */
/* other code portions copyright http://dada.perl.it/shootout/ and Doug Bagley http://www.bagley.org/~doug/shootout */
/* combined, modified and fixed by Thomas Bruckschlegel - http://www.tommti-systems.com */
using System;
using System.Collections;
using System.Text;
using System.IO;

namespace Benchmark_CSharp
{
	class BenchmarkCSharp
	{
		static DateTime startTime;
		static DateTime stopTime;
		static TimeSpan elapsedTime;
		
		[STAThread]
		static void Main(string[] args)
		{
			int intMax = 		1000000000; // 1B
			double doubleMin = 	10000000000.0D; // 10B
			double doubleMax = 	11000000000.0D; // 11B
			long longMin = 		10000000000L; // 10B
			long longMax = 		11000000000L; // 11B
			double trigMax = 	10000000.0; // 10M
			int ioMax =			1000000; // 1M

	
			Console.WriteLine("Start C# benchmark");
			long intArithmeticTime = (long)intArithmetic(intMax);
			long doubleArithmeticTime = (long)doubleArithmetic(doubleMin, doubleMax);
			long longArithmeticTime = (long)longArithmetic(longMin, longMax);
			long trigTime = (long)trig(trigMax);
			long ioTime = (long)io(ioMax);
			
			int n=10;
			
			long arrayTime = (long)array(n*10000);
			long ExceptionTime = (long)except(n*100000);
			long hashtestTime = (long)hashtest(n*10000);
			long hashesTime = (long)hashes(n*100);
			long hsTime = (long)hs(n*100000);
			long vsTime = (long)vs(n*10);
			long mmTime = (long)mm(n*10000);		
			long nlTime = (long)nl(n*4);			
			long scTime = (long)sc(n*1000000);

			
			long totalTime =intArithmeticTime + doubleArithmeticTime + longArithmeticTime + trigTime + ioTime
				+arrayTime
				+ExceptionTime
				+hashtestTime
				+hashesTime
				+hsTime 
				+vsTime 
				+mmTime 
				+nlTime 
				+scTime; 

			Console.WriteLine("Total C# benchmark time: " + totalTime + " ms");
			Console.WriteLine("End C# benchmark");
			Console.ReadLine();
		}


		/*
		 * Math benchmark using ints.
		 */
		static long intArithmetic(int intMax)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;
		
			int intResult = 1;
			int i = 0;
			while (i < intMax)
			{
				intResult -= i++;
				intResult += i++;
				intResult *= i++;
				intResult /= i++;
			}

			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;

			Console.WriteLine("Int arithmetic elapsed time: " + elapsedMilliseconds + 
				" ms with max of " + intMax);
			Console.WriteLine(" i: " + i);
			Console.WriteLine(" intResult: " + intResult);
			return elapsedMilliseconds;
		}


		/**
		 * Math benchmark using doubles.
		 */
		static long doubleArithmetic(double doubleMin, double doubleMax)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			double doubleResult = doubleMin;
			double i = doubleMin;
			while (i < doubleMax)
			{
				doubleResult -= i++;
				doubleResult += i++;
				doubleResult *= i++;
				doubleResult /= i++;
			}
		
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
		
			Console.WriteLine("Double arithmetic elapsed time: " + elapsedMilliseconds + 
				" ms with min of " + doubleMin + ", max of " + doubleMax);
			Console.WriteLine(" i: " + i);
			Console.WriteLine(" doubleResult: " + doubleResult);
			return elapsedMilliseconds;
		}


		/**
		 * Math benchmark using longs.
		 */
		static long longArithmetic(long intMin, long intMax)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			long intResult = intMin;
			long i = intMin;
			while (i < intMax)
			{
				intResult -= i++;
				intResult += i++;
				intResult *= i++;
				intResult /= i++;
			}

			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
		
			Console.WriteLine("long arithmetic elapsed time: " + elapsedMilliseconds + 
				" ms with min of " + intMin + ", max of " + intMax);
			Console.WriteLine(" i: " + i);
			Console.WriteLine(" intResult: " + intResult);
			return elapsedMilliseconds;
		}


		/**
		 * Calculate sin, cos, tan, log, square root for all numbers up to a max.
		 */
		static long trig(double trigMax)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;
		
			double sine = 0.0D;
			double cosine = 0.0D;
			double tangent = 0.0D;
			double logarithm = 0.0D;
			double squareRoot = 0.0D;		
			double i = 0.0D;
			while(i < trigMax)
			{
				sine = Math.Sin(i);
				cosine = Math.Cos(i);
				tangent = Math.Tan(i);
				logarithm = Math.Log10(i);
				squareRoot = Math.Sqrt(i);
				i++;
			}
		
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
		
			Console.WriteLine("Trig elapsed time: " + elapsedMilliseconds + 
				" ms with max of " + trigMax);
			Console.WriteLine(" i: " + i);
			Console.WriteLine(" sine: " + sine);
			Console.WriteLine(" cosine: " + cosine);
			Console.WriteLine(" tangent: " + tangent);
			Console.WriteLine(" logarithm: " + logarithm);
			Console.WriteLine(" squareRoot: " + squareRoot);
			return elapsedMilliseconds;
		}


		/**
		 * Write max lines to a file, then read max lines back in from file.
		 */
		static long io(int ioMax)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;
		
			String fileName = "C:\\TestCSharp.txt";
			String textLine = "abcdefghijklmnopqrstuvwxyz1234567890abcdefghijklmnopqrstuvwxyz1234567890abcdefgh";
			int i = 0;
			String myLine = "";
		
			try
			{	
				StreamWriter streamWriter = new StreamWriter(fileName);
				while (i++ < ioMax)
				{
					streamWriter.WriteLine(textLine);
				}
				streamWriter.Close();

				i = 0;
				StreamReader streamReader = new StreamReader(fileName);
				while (i++ < ioMax) 
				{
					myLine = streamReader.ReadLine();
				}
			}
			catch (IOException e)
			{
				System.Console.Write(e.Message);
			}
		
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
				
			Console.WriteLine("IO elapsed time: " + elapsedMilliseconds + 
				" ms with max of " + ioMax);
			Console.WriteLine(" i: " + i);
			Console.WriteLine(" myLine: " + myLine);
			return elapsedMilliseconds;
		}

		///// update
		//////////////
		static long array(int n)
		{        
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			int i, j, k;
			int[] x;
			int[] y;
        
			if(n < 1) n = 1;
        
			x = new int[n];
			y = new int[n];
        
			for (i = 0; i < n; i++)
			{
				x[i] = i + 1;
				y[i] = 0;
			}
			for (k = 0; k < 1000; k++ )
				for (j = n-1; j >= 0; j--)
					y[j] += x[j];
        
			//Console.WriteLine(y[0].ToString() + " " + y[n-1].ToString());
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
				
			Console.WriteLine("Array elapsed time: " + elapsedMilliseconds + " ms - "+ y[0].ToString() + " " + y[n-1].ToString());
			return elapsedMilliseconds;
		}	

		class LoException : System.Exception
		{
			public LoException(){}
		}

		class HiException : System.Exception 
		{
			public HiException(){}
		}

		static int Lo = 0;
		static int Hi = 0;
		
		//////////////
		static long except(int n)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			while(n!=0) 
			{
				SomeFunction(n);
				n--;
			}

			//			System.Text.StringBuilder bldr = new System.Text.StringBuilder(100);
			//			bldr.Append("Exceptions: HI=").Append(Hi).Append(" / LO=").Append(Lo);
			//			Console.WriteLine(bldr.ToString());
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
				
			Console.WriteLine("Exception elapsed time: " + elapsedMilliseconds + " ms - "+"Exceptions: HI="+Hi.ToString()+" / LO="+Lo.ToString());
			return elapsedMilliseconds;
		}

		public static void SomeFunction(int n) 
		{
			try 
			{
				HiFunction(n);
			} 
			catch (Exception e) 
			{
				Console.WriteLine("We shouldn't get here: " + e.Message);
			}
		}

		public static void HiFunction(int n) 
		{
			try 
			{
				LoFunction(n);
			} 
			catch (HiException) 
			{
				Hi++;
			}
		}
		public static void LoFunction(int n)
		{
			try 
			{
				BlowUp(n);
			} 
			catch (LoException) 
			{
				Lo++;
			}
		}
		public static void BlowUp(int n) 
		{
			if ((n & 1) == 0) 
			{
				throw new LoException();
			} 
			else 
			{
				throw new HiException();
			}
		}	

		static long hashtest(int n) 
		{        
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			Hashtable X = new Hashtable();     
			int c = 0;
        
			if(n < 1) n = 1;
        
			for(int i=1; i<=n; i++) 
			{
				X.Add( i.ToString("x"), i);
			}
        
			for(int i=(int)n; i>0; i--) 
			{
				if(X.ContainsKey(i.ToString())) c++;
			}
			//Console.WriteLine(c.ToString());
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
				
			Console.WriteLine("HashMap elapsed time: " + elapsedMilliseconds + " ms - "+c.ToString());
			return elapsedMilliseconds;
		}
		static long hashes(int n)
		{        
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			Hashtable hash1 = new Hashtable();
			Hashtable hash2 = new Hashtable();
                      
			for(int i=0; i<10000; i++) 
			{
				hash1.Add( "foo_" + i.ToString(), i);
			}
        
                        for(int i=0; i<n; i++)
			{
				IDictionaryEnumerator it = hash1.GetEnumerator();
				while(it.MoveNext()) 
				{
					object v2 = hash2[it.Key];                
					if(v2!=null) 
					{
						hash2[it.Key]= ((int)v2)+(int)it.Value;
					} 
					else 
					{
						hash2.Add(it.Key, it.Value);
					}
				}
			}
			//Console.WriteLine(hash1["foo_1"] + " " + hash1["foo_9999"] + " " + hash2["foo_1"] + " " + hash2["foo_9999"]);
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
				
			Console.WriteLine("HashMaps elapsed time: " + elapsedMilliseconds + " ms - "+hash1["foo_1"] + " " + hash1["foo_9999"] + " " + hash2["foo_1"] + " " + hash2["foo_9999"]);
			return elapsedMilliseconds;
		}	

		public const int IM = 139968;
		public const int IA =   3877;
		public const int IC =  29573;

		public static int last = 42;
        
		public static double gen_random(double max) 
		{
			return( max * (last = (last * IA + IC) % IM) / IM );
		}

		///////////////////
		static long hs(int count)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;
			double[] ary = new double[count+1];
			//unsafe
			//{
			for(int i=0;i<=count;++i)
			{
				ary[i]=gen_random(1);
			}
			//}
			heapsort(ary);
			//Console.WriteLine(ary[count]);
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;
				
			Console.WriteLine("HeapSort elapsed time: " + elapsedMilliseconds + " ms - "+ary[count]);
			return elapsedMilliseconds;
		}
    
		public static void heapsort(double[] ra) 
		{
			//unsafe
			//{
			int l=0, j=0, ir=0, i=0;
			double rra=0.0;

			l = ((ra.Length-1) >> 1) + 1;
			ir = (ra.Length-1);
			for (;;) 
			{
				if (l > 1) 
				{
					rra = ra[--l];
				} 
				else 
				{
					rra = ra[ir];
					ra[ir] = ra[1];
					if (--ir == 1) 
					{
						ra[1] = rra;
						return;
					}
				}
				i = l;
				j = l << 1;
				while (j <= ir) 
				{
					if (j < ir && ra[j] < ra[j+1]) { ++j; }
					if (rra < ra[j]) 
					{
						ra[i] = ra[j];
						j += (i = j);
					} 
					else 
					{
						j = ir + 1;
					}
				}
				ra[i] = rra;
			}
			//}
		}	
	
	
		public const int lSIZE=10000;
		static long vs(int n)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			int result=0;
			for(int i=0;i<n;++i)
				result = VectorTest();
			//Console.WriteLine(result);
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;			
			Console.WriteLine("Vector elapsed time: " + elapsedMilliseconds + " ms - "+result);
			
			return elapsedMilliseconds;
		}
		static public int VectorTest()
		{
			// create a list of integers (Li1) from 1 to SIZE
			//LinkedList Li1 = new LinkedList();
			ArrayList Li1 = new ArrayList();
			for (int i = 1; i < lSIZE + 1; i++)
			{
				Li1.Insert(Li1.Count, i);	//addlast
				//Li1.addLast(new Integer(i));
			}
			// copy the list to Li2 (not by individual items)
			ArrayList Li2 = new ArrayList(Li1);
			ArrayList Li3 = new ArrayList();
			// remove each individual item from left side of Li2 and
			// append to right side of Li3 (preserving order)
			while (Li2.Count>0)
			{
				Li3.Insert(Li3.Count, Li2[0]); //addlast
				Li2.RemoveAt(0);
			}
			// Li2 must now be empty
			// remove each individual item from right side of Li3 and
			// append to right side of Li2 (reversing list)
			while (Li3.Count>0)
			{
				Li2.Insert(Li2.Count, Li3[Li3.Count-1]);	//addlast
				Li3.RemoveAt(Li3.Count-1);
			}
			// Li3 must now be empty
			// reverse Li1
			ArrayList tmp = new ArrayList();
			while (Li1.Count>0)
			{
				tmp.Insert(0, Li1[0]); //addfirst
				Li1.RemoveAt(0);
			}
			Li1 = tmp;
			// check that first item is now lSIZE
			if ( (int)(Li1[0]) != lSIZE )
			{
				Console.WriteLine("first item of Li1 != lSIZE");
				return (0);
			}
			// compare Li1 and Li2 for equality
			// where is the == operator?
			// do a Li1!=Li2 comparison
			if(Li1.Count!=Li2.Count)
			{
				Console.WriteLine("Li1 and Li2 differ");
				return (0);
			}
			for(int i=0; i<Li1.Count; i++)
			{
				if(Li1[i]!=Li2[i])
				{
					Console.WriteLine("Li1 and Li2 differ");
					return (0);
				}
			}
			// return the length of the list
			return (Li1.Count);
		}
	
		static int mSIZE = 30;

		public static int[,] mkmatrix (int rows, int cols) 
		{
			int count = 1;
			int[,] m = new int[rows,cols];
			for (int i=0; i<rows; i++) 
			{
				for (int j=0; j<cols; j++) 
				{
					m[i,j] = count++;
				}
			}
			return(m);
		}

		public static void mmult (int rows, int cols, 
			int[,] m1, int[,] m2, int[,] m3) 
		{
			for (int i=0; i<rows; i++) 
			{
				for (int j=0; j<cols; j++) 
				{
					int val = 0;
					for (int k=0; k<cols; k++) 
					{
						val += m1[i,k] * m2[k,j];
					}
					m3[i,j] = val;
				}
			}
		}

		public static long mm(int n)
		{        
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			int[,] m1 = mkmatrix(mSIZE, mSIZE);
			int[,] m2 = mkmatrix(mSIZE, mSIZE);
			int[,] mm = new int[mSIZE,mSIZE];
        
			if(n < 1) n = 1;
        
			for (int i=0; i<n; i++) 
			{
				mmult(mSIZE, mSIZE, m1, m2, mm);
			}
        
			//			Console.WriteLine(
			//				mm[0,0].ToString() + " " + mm[2,3].ToString() + " " +
			//				mm[3,2].ToString() + " " + mm[4,4].ToString());
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;			
			Console.WriteLine("Matrix Multiply elapsed time: " + elapsedMilliseconds + " ms - "+
				mm[0,0].ToString() + " " + mm[2,3].ToString() + " " +
				mm[3,2].ToString() + " " + mm[4,4].ToString());

			return elapsedMilliseconds;
		}		
		
		public static long nl(int n)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			int a, b, c, d, e, f;
			int x=0;

			if(n < 1) n = 1;

			for (a=0; a<n; a++)
			{
				for (b=0; b<n; b++)
				{
					for (c=0; c<n; c++)
					{
						for (d=0; d<n; d++)
						{
							for (e=0; e<n; e++)
							{
								for (f=0; f<n; f++)
								{
									x+=a+b+c+d+e+f;
								}
							}
						}
					}
				}
			}
    
			//			Console.WriteLine(x.ToString() + "\n");
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;			
			Console.WriteLine("Nested Loop elapsed time: " + elapsedMilliseconds + " ms - "+x.ToString());

			return elapsedMilliseconds;
		}		
		
		
		
		public static long sc(int N)
		{
			long elapsedMilliseconds;
			startTime = DateTime.Now;

			if(N < 1) N = 1;

			StringBuilder sb = new StringBuilder((int)N*8);

			for (int i=0; i<N; i++) 
			{
				sb.Append("hello");
			}

			//			Console.WriteLine(sb.Length);
			stopTime = DateTime.Now;
			elapsedTime = stopTime.Subtract(startTime);
			elapsedMilliseconds = (int)elapsedTime.TotalMilliseconds;			
			Console.WriteLine("String Concat. (fixed) elapsed time: " + elapsedMilliseconds + " ms - "+sb.Length);

			return elapsedMilliseconds;
		}

		//end
	}
}
