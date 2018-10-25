/* original code copyright 2004 Christopher W. Cowell-Shah http://www.cowell-shah.com/research/benchmark/code */
/* other code portions copyright http://dada.perl.it/shootout/ and Doug Bagley http://www.bagley.org/~doug/shootout */
/* combined, modified and fixed by Thomas Bruckschlegel - http://www.tommti-systems.com */

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.text.NumberFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Vector;

public class Benchmark
{
    static int Lo = 0;
    static int Hi = 0;

    static long startTime;
    static long stopTime;
    static long elapsedTime;

    public static void main(String[] args) throws IOException
    {
        int intMax = 1000000000; // 1B
        double doubleMin = 10000000000.0; // 10B
        double doubleMax = 11000000000.0; // 11B
        long longMin = 10000000000L; // 10B
        long longMax = 11000000000L; // 11B
        double trigMax = 10000000.0; // 10M
        int ioMax = 1000000; // 1M

        System.out.println("Start Java benchmark");

        long intArithmeticTime = intArithmetic(intMax);
        long doubleArithmeticTime = doubleArithmetic(doubleMin, doubleMax);
        long longArithmeticTime = longArithmetic(longMin, longMax);
        long trigTime = trig(trigMax);
        long ioTime = io(ioMax);

        int n = 10;
        long arrayTime = (long) array(n * 10000);
        long ExceptionTime = (long) except(n * 100000);
        long hashtestTime = (long) hashtest(n * 10000);
        long hashesTime = (long) hashes(n * 100);
        long hsTime = (long) hs(n * 100000);
        long vsTime = (long) vs(n * 10);
        long mmTime = (long) mm(n * 10000);
        long nlTime = (long) nl(n * 4);
        long scTime = (long) sc(n * 1000000);

        long totalTime = intArithmeticTime + doubleArithmeticTime + longArithmeticTime + trigTime + ioTime
            + arrayTime
            + ExceptionTime
            + hashtestTime
            + hashesTime
            + hsTime
            + vsTime
            + mmTime
            + nlTime
            + scTime;

        System.out.println("Total Java benchmark time: " + totalTime + " ms");
        System.out.println("End Java benchmark");
        System.in.read();
    }

    /**
     * Math benchmark using ints.
     */
    static long intArithmetic(int intMax)
    {
        startTime = (new Date()).getTime();

        int intResult = 1;
        int i = 0;
        while (i < intMax)
        {
            intResult -= i++;
            intResult += i++;
            intResult *= i++;
            intResult /= i++;
        }

        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;

        System.out.println("Int arithmetic elapsed time: " + elapsedTime +
                           " ms with max of " + intMax);
        System.out.println(" i: " + i + "\n" + " intResult: " + intResult);
        return elapsedTime;
    }

    /**
     * Math benchmark using doubles.
     */
    static long doubleArithmetic(double doubleMin, double doubleMax)
    {
        startTime = (new Date()).getTime();

        double doubleResult = doubleMin;
        double i = doubleMin;
        while (i < doubleMax)
        {
            doubleResult -= i++;
            doubleResult += i++;
            doubleResult *= i++;
            doubleResult /= i++;
        }

        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;

        System.out.println("Double arithmetic elapsed time: " + elapsedTime +
                           " ms with min of " + doubleMin + ", max of " + doubleMax);
        System.out.println(" i: " + i + "\n" + " doubleResult: " + doubleResult);
        return elapsedTime;
    }

    /**
     * Math benchmark using longs.
     */
    static long longArithmetic(long longMin, long longMax)
    {
        startTime = (new Date()).getTime();

        long longResult = longMin;
        long i = longMin;
        while (i < longMax)
        {
            longResult -= i++;
            longResult += i++;
            longResult *= i++;
            longResult /= i++;
        }

        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;

        System.out.println("Long arithmetic elapsed time: " + elapsedTime +
                           " ms with min of " + longMin + ", max of " + longMax);
        System.out.println(" i: " + i);
        System.out.println(" longResult: " + longResult);
        return elapsedTime;
    }

    /**
     * Calculate sin, cos, tan, log, square root
     * for all numbers up to a max.
     */
    static long trig(double trigMax)
    {
        startTime = (new Date()).getTime();

        double sine = 0.0;
        double cosine = 0.0;
        double tangent = 0.0;
        double logarithm = 0.0;
        double squareRoot = 0.0;
        double i = 0.0;
        double conversion = 1.0 / Math.log(10.0);
        while (i < trigMax)
        {
            sine = Math.sin(i);
            cosine = Math.cos(i);
            tangent = Math.tan(i);
            logarithm = Math.log(i) * conversion;
            squareRoot = Math.sqrt(i);
            i++;
        }

        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;

        System.out.println("Trig elapsed time: " + elapsedTime +
                           " ms with max of " + trigMax);
        System.out.println(" i: " + i);
        System.out.println(" sine: " + sine);
        System.out.println(" cosine: " + cosine);
        System.out.println(" tangent: " + tangent);
        System.out.println(" logarithm: " + logarithm);
        System.out.println(" squareRoot: " + squareRoot);
        return elapsedTime;
    }

    /**
     * Write max lines to a file, then read max lines back from file.
     */
    static long io(int ioMax)
    {
        startTime = (new Date()).getTime();

        final String textLine =
            "abcdefghijklmnopqrstuvwxyz1234567890abcdefghijklmnopqrstuvwxyz1234567890abcdefgh\n";
        int i = 0;
        String myLine = "";

        try
        {
            File file = new File("C:\\TestJava.txt");
            FileWriter fileWriter = new FileWriter(file);
            BufferedWriter bufferedWriter = new BufferedWriter(fileWriter);
            while (i++ < ioMax)
            {
                bufferedWriter.write(textLine);
            }
            bufferedWriter.close();
            fileWriter.close();

            FileReader inputFileReader = new FileReader(file);
            BufferedReader bufferedReader =
                new BufferedReader(inputFileReader);
            i = 0;
            while (i++ < ioMax)
            {
                myLine = bufferedReader.readLine();
            }
            bufferedReader.close();
            inputFileReader.close();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }

        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("IO elapsed time: " + elapsedTime +
                           " ms with max of " + ioMax);
        System.out.println(" i: " + i);
        System.out.println(" myLine: " + myLine);
        return elapsedTime;
    }

    ///// update
    ///////////////
    static long array(int n)
    {
        startTime = (new Date()).getTime();
        int i, j, k;
        int x[] = new int[n];
        int y[] = new int[n];

        for (i = 0; i < n; i++)
        {
            x[i] = i + 1;
            y[i] = 0;
        }
        for (k = 0; k < 1000; k++)
        {
            for (j = n - 1; j >= 0; j--)
            {
                y[j] += x[j];

//          System.out.println(y[0] + " " + y[n-1]);
            }
        }
        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("Array elapsed time: " + elapsedTime + " ms - " + y[n - 1]);
        return elapsedTime;
    }

    static class Lo_Exception
        extends Exception
    {
        int num = 0;
        public Lo_Exception(int num)
        {
            this.num = num;
        }

        public String toString()
        {
            return "Lo_Exception, num = " + this.num;
        }
    }

    static class Hi_Exception
        extends Exception
    {
        int num = 0;
        public Hi_Exception(int num)
        {
            this.num = num;
        }

        public String toString()
        {
            return "Hi_Exception, num = " + this.num;
        }
    }

    ////////////////
    static long except(int n) throws IOException
    {
        startTime = (new Date()).getTime();

        for (int i = 0; i < n; i++)
        {
            some_function(i);
        }
//            System.out.println("Exceptions: HI=" + Hi + " / LO=" + Lo);
        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("Exception elapsed time: " + elapsedTime + " ms - " + "HI=" + Hi + " / LO=" + Lo);
        return elapsedTime;
    }

    static void some_function(int n)
    {
        try
        {
            hi_function(n);
        }
        catch (Exception e)
        {
            System.out.println("We shouldn't get here: " + e);
        }
    }

    static void hi_function(int n) throws Hi_Exception, Lo_Exception
    {
        try
        {
            lo_function(n);
        }
        catch (Hi_Exception e)
        {
            Hi++;
        }
    }

    static void lo_function(int n) throws Hi_Exception, Lo_Exception
    {
        try
        {
            blowup(n);
        }
        catch (Lo_Exception e)
        {
            Lo++;
        }
    }

    static void blowup(int n) throws Hi_Exception, Lo_Exception
    {
        if ( (n & 1) == 1)
        {
            throw new Lo_Exception(n);
        }
        else
        {
            throw new Hi_Exception(n);
        }
    }

    static long hashtest(int n) throws IOException
    {
        startTime = (new Date()).getTime();
        int i, c;
        String s = "";
        Integer ii;
        // the original program used:
        // Hashtable ht = new Hashtable();
        // John Olsson points out that Hashtable is for synchronized access
        // and we should use instead:
        HashMap ht = new HashMap();

        c = 0;
        for (i = 1; i <= n; i++)
        {
            ht.put(Integer.toString(i, 16), new Integer(i));
        }
        for (i = 1; i <= n; i++)
        {

            // The original code converted to decimal string this way:
            // if (ht.containsKey(i+""))
            if (ht.containsKey(Integer.toString(i, 10)))
            {
                c++;

//              System.out.println(c);
            }
        }
        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("HashMap elapsed time: " + elapsedTime + " ms - " + c);
        return elapsedTime;
    }

    static class Val
    {
        int val;
        Val(int init)
        {
            val = init;
        }
    }

    static long hashes(int n)
    {
        startTime = (new Date()).getTime();
        HashMap hash1 = new HashMap(10000);
        HashMap hash2 = new HashMap(n);

        for (int i = 0; i < 10000; i++)
        {
            hash1.put("foo_" + Integer.toString(i, 10), new Val(i));
        }

        for (int i = 0; i < n; i++)
        {
            Iterator it = hash1.entrySet().iterator();

            while (it.hasNext())
            {
                Map.Entry h1 = (Map.Entry) it.next();
                String key = (String) h1.getKey();
                int v1 = ( (Val) h1.getValue()).val;
                if (hash2.containsKey(key))
                {
                    ( (Val) hash2.get(key)).val += v1;
                }
                else
                {
                    hash2.put(key, new Val(v1));
                }
            }
        }

        //                  System.out.print(((Val)hash1.get("foo_1")).val    + " " +
        //                                 ((Val)hash1.get("foo_9999")).val + " " +
        //                               ((Val)hash2.get("foo_1")).val    + " " +
        //                             ((Val)hash2.get("foo_9999")).val + "\n");

        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("HashMaps elapsed time: " + elapsedTime + " ms - " +
                           ( (Val) hash1.get("foo_1")).val + " " +
                           ( (Val) hash1.get("foo_9999")).val + " " +
                           ( (Val) hash2.get("foo_1")).val + " " +
                           ( (Val) hash2.get("foo_9999")).val + "\n");
        return elapsedTime;
    }

    static long IM = 139968;
    static long IA = 3877;
    static long IC = 29573;

    static long hs(int N)
    {
        startTime = (new Date()).getTime();
        NumberFormat nf = NumberFormat.getInstance();
        nf.setMaximumFractionDigits(10);
        nf.setMinimumFractionDigits(10);
        nf.setGroupingUsed(false);
        double[] ary = new double[N + 1];
        for (int i = 1; i <= N; i++)
        {
            ary[i] = gen_random(1);
        }
        heapsort(ary);
//        System.out.print(nf.format(ary[N]) + "\n");
        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("HeapSort elapsed time: " + elapsedTime + " ms - " + nf.format(ary[N]));
        return elapsedTime;
    }

    static long last = 42;
    static double gen_random(double max)
    {
        return (max * (last = (last * IA + IC) % IM) / IM);
    }

    static void heapsort(double ra[])
    {
        int l, j, ir, i;
        double rra;

        l = ( (ra.length - 1) >> 1) + 1;
        ir = (ra.length - 1);
        for (; ; )
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
                if (j < ir && ra[j] < ra[j + 1])
                {
                    ++j;
                }
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
    }

    static int lSIZE = 10000;

    static long vs(int n)
    {
        startTime = (new Date()).getTime();
        int result = 0;
        for (int i = 0; i < n; i++)
        {
            result = VectorTest();
        }
//        System.out.println(result);
        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("Vector elapsed time: " + elapsedTime + " ms - " + result);
        return elapsedTime;
    }

    static int VectorTest()
    {
        int result = 0;
        // create a list of integers (Li1) from 1 to SIZE

        Vector Li1 = new Vector();
        for (int i = 1; i < lSIZE + 1; i++)
        {
            Li1.add(new Integer(i));
            //Li1.addLast(new Integer(i));
        }
        // copy the list to Li2 (not by individual items)
        Vector Li2 = new Vector(Li1);
        Vector Li3 = new Vector();
        // remove each individual item from left side of Li2 and
        // append to right side of Li3 (preserving order)
        while (!Li2.isEmpty())
        {
            Li3.add(Li2.remove(0));
        }
        // Li2 must now be empty
        // remove each individual item from right side of Li3 and
        // append to right side of Li2 (reversing list)
        while (!Li3.isEmpty())
        {
            Li2.add(Li3.lastElement());
            Li3.removeElementAt(Li3.size() - 1);
        }
        // Li3 must now be empty
        // reverse Li1
        Vector tmp = new Vector();
        while (!Li1.isEmpty())
        {
            tmp.add(0, Li1.remove(0));
        }
        Li1 = tmp;
        // check that first item is now SIZE
        //        if ( ( (Integer) Li1.getFirst()).intValue() != lSIZE)
        if ( ( (Integer) Li1.get(0)).intValue() != lSIZE)
        {
            System.err.println("first item of Li1 != lSIZE");
            return (0);
        }
        // compare Li1 and Li2 for equality
        if (!Li1.equals(Li2))
        {
            System.err.println("Li1 and Li2 differ");
            System.err.println("Li1:" + Li1);
            System.err.println("Li2:" + Li2);
            return (0);
        }
        // return the length of the list
        return (Li1.size());
    }

    static int mSIZE = 30;

    static long mm(int n)
    {
        startTime = (new Date()).getTime();
        int m1[][] = mkmatrix(mSIZE, mSIZE);
        int m2[][] = mkmatrix(mSIZE, mSIZE);
        int mm[][] = new int[mSIZE][mSIZE];
        for (int i = 0; i < n; i++)
        {
            mmult(mSIZE, mSIZE, m1, m2, mm);
        }
        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("Matrix Multiply elapsed time: " + elapsedTime + " ms - " +
                           mm[0][0] + " " + mm[2][3] + " " + mm[3][2] + " " + mm[4][4]);
        return elapsedTime;
    }

    static int[][] mkmatrix(int rows, int cols)
    {
        int count = 1;
        int m[][] = new int[rows][cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                m[i][j] = count++;
            }
        }
        return (m);
    }

    static void mmult(int rows, int cols, int[][] m1, int[][] m2, int[][] m3)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int val = 0;
                for (int k = 0; k < cols; k++)
                {
                    val += m1[i][k] * m2[k][j];
                }
                m3[i][j] = val;
            }
        }
    }

    static long nl(int n) throws IOException
    {
        startTime = (new Date()).getTime();
        int x = 0;
        for (int a = 0; a < n; a++)
        {
            for (int b = 0; b < n; b++)
            {
                for (int c = 0; c < n; c++)
                {
                    for (int d = 0; d < n; d++)
                    {
                        for (int e = 0; e < n; e++)
                        {
                            for (int f = 0; f < n; f++)
                            {
                                x += a + b + c + d + e + f;
                            }
                        }
                    }
                }
            }
        }
        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("Nested Loop elapsed time: " + elapsedTime + " ms - " + x);
        return elapsedTime;
    }

    static long sc(int n) throws IOException
    {
        startTime = (new Date()).getTime();
        StringBuffer stringBuffer = new StringBuffer(8 * n);

        for (int i = 0; i < n; i++)
        {
            stringBuffer.append("hello");
        }

        //System.out.println(stringBuffer.toString().length());
        stopTime = (new Date()).getTime();
        elapsedTime = stopTime - startTime;
        System.out.println("String Concat. (fixed) elapsed time: " + elapsedTime + " ms - " + stringBuffer.length());
        return elapsedTime;
    }

}
