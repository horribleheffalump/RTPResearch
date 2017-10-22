using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using MathWorks.MATLAB.NET.Arrays;

namespace RTPClasses
{
    public class Sample
    {
        public double Instant;
        public double PreviousObservationInstant;

        public int StateNumber;
        public int ObservationNumber;
        public Matrix<double> State;
        public Matrix<double> Observation;
        public RTPFrame Frame;


        public Matrix<double> XHat;


        public Sample(double instant, double previousObservationInstant, int stateNum, Matrix<double> state, int observationNum, Matrix<double> observation, RTPFrame frame)
        {
            Instant = instant;
            PreviousObservationInstant = previousObservationInstant;
            //Delay = delay;
            StateNumber = stateNum;
            State = state;
            ObservationNumber = observationNum;
            Observation = observation;
            Frame = frame;

        }
    }

    public class GammaParams
    {
        public double a;
        public double b;

        public GammaParams(double _a, double _b)
        {
            a = _a;
            b = _b;
        }
    }

    public class PiParams
    {
        public double Norm;
        public double ExpLambda = 0; // если используем экспоненциальное
        public GammaParams Gamma; // если используем гамма-распределение

        public PiParams(double _norm, GammaParams _gamma)
        {
            Norm = _norm;
            Gamma = _gamma;
        }
    }


    public class RTPModel
    {
        //public RTPSession Session;
        public List<RTPState> States;
        public List<RTPObservation> Observations;
        public List<Sample> ModelSamples;
        public List<Sample> SystemSamples;
        public List<Sample> EstimateSamples;
        //private int lastProcessedSystemSample = 0;
        //public int lastEstimatedSample = 0;

        public int N = 3; //number of states
        public int M = 3; //number of observations
        public Matrix<double>[] E;
        public Matrix<double>[] F;

        public double RSM_lb;
        public double RSM_ub;
        public int PC_b;

        public double[] TimesInState;
        public int[,] TransitionCount;
        //public double[,] Lambda;
        public Matrix<double> Lambda;

        public MatrixBuilder<double> MBuilder;
        public VectorBuilder<double> VBuilder;

        public double step;
        //public double estimationStartInstance = 0;

        //public List<double[]> StateDencities; // параметры Гамма-распределений плотностей 
        //public GammaParams[,] Dencities; // параметры Гамма-распределений плотностей 

        //public double[, ,] Pi; //условные вероятности переключения наблюдений при фиксированном состоянии  Pi[i,j,k] = P{Yn = fi | Yn-1 = fj, Xn-1 = ek} 
        public List<double>[,,] GammaSamples; // параметры Гамма-распределений плотностей   Pi[j,k,l] = P{Yn = fj | Yn-1 = fk, Xn-1 = el} 
        //public GammaParams[, ,] GammaDensities; // параметры Гамма-распределений плотностей   Pi[j,k,l] = P{Yn = fj | Yn-1 = fk, Xn-1 = el} 
        public PiParams[,,] CDFParams; // параметры условных плотностей: Pi[j,k,l] = P{Yn = fj | Yn-1 = fk, Xn-1 = el} = G[j,k,l]*Norm[j,k,l] = G[j,k,l] * N[j,k,l]/sum_j(N[j,k,l])

        public List<double>[] TimeInStateSamples;
        public double[] TimesInStateCDFParams;

        private MLApp.MLApp Matlab;
        //private MatlabFunctions.MatlabFunctions matlabFunctions;

        public RTPModel()
        {
            Init(0,0,0);
     
            ModelParamsInit();


            //matlabFunctions = new MatlabFunctions.MatlabFunctions();

            {
                // старая инициализация, в которой делалось всё
                //AssignStatesAndObservations(sessionModel, ModelSamples);
                //AssignStatesAndObservations(sessionSystem, SystemSamples);

                //TransitionMatrixEstimate();


                //CDFParams = CDFParamsEstimate();

                //ExportGammaData(folderOut);
                //ExportTimesInStateData(folderOut);



                //Estimate();
                //SaveEstimate(folderOut);
            }

            {
                // медленное заполнение EstimateSamples
                //double finish = SystemSamples.Max(s => s.Instant);
                //Sample sampleNow = SystemSamples[0];
                //TestEstimateSamples.Add(SystemSamples[0]);
                //for (double curTime = start + step; curTime < finish; curTime += step)
                //{
                //    double lastInstant = SystemSamples.Where(s => s.Instant < curTime).Max(s => s.Instant);
                //    if (lastInstant > sampleNow.Instant)
                //    {
                //        sampleNow = SystemSamples.First(s => s.Instant == lastInstant);
                //        foreach (Sample s in SystemSamples.Where(s => s.Instant > curTime - step && s.Instant < curTime).OrderBy(s => s.Instant))
                //        {
                //            TestEstimateSamples.Add(s);
                //        }
                //    }
                //    TestEstimateSamples.Add(new Sample(curTime, sampleNow.StateNumber, sampleNow.State, sampleNow.ObservationNumber, sampleNow.Observation, null));
                //}

                //bool equal = true;
                //for (int i = 0; i < TestEstimateSamples.Count; i++)
                //{
                //    if (TestEstimateSamples[i].Instant != EstimateSamples[i].Instant) equal = false;
                //    if (TestEstimateSamples[i].ObservationNumber != EstimateSamples[i].ObservationNumber) equal = false;
                //    if (TestEstimateSamples[i].StateNumber != EstimateSamples[i].StateNumber) equal = false;
                //    if (!equal)
                //    {
                //        break;
                //    }

                //}
                //}
            }
        }

        public void Init(double rsm_lb, double rsm_ub, int pc_b)
        {
            step = 0.01;
            States = new List<RTPState>();
            Observations = new List<RTPObservation>();
            ModelSamples = new List<Sample>();
            SystemSamples = new List<Sample>();
            EstimateSamples = new List<Sample>();
            //TestEstimateSamples = new List<Sample>();

            MBuilder = Matrix<double>.Build;
            VBuilder = Vector<double>.Build;

            E = new Matrix<double>[N];
            F = new Matrix<double>[M];

            for (int i = 0; i < N; i++)
            {
                E[i] = MBuilder.Dense(N, 1);
                E[i][i, 0] = 1;
            }
            for (int i = 0; i < M; i++)
            {
                F[i] = MBuilder.Dense(M, 1);
                F[i][i, 0] = 1;
            }

            Matlab = new MLApp.MLApp();

            DefineStates(rsm_lb, rsm_ub);
            DefineObservations(pc_b);   

            //ModelParamsInit();
        }

        private void ModelParamsInit()
        {
            TimesInState = new double[States.Count];
            TransitionCount = new int[States.Count, States.Count];
            for (int i = 0; i < States.Count; i++)
                for (int j = 0; j < States.Count; j++)
                    TransitionCount[i, j] = 0;

            GammaSamples = new List<double>[Observations.Count, Observations.Count, States.Count];
            for (int l = 0; l < States.Count; l++)
                for (int k = 0; k < Observations.Count; k++)
                    for (int j = 0; j < Observations.Count; j++)
                    {
                        GammaSamples[j, k, l] = new List<double>();
                    }

            TimeInStateSamples = new List<double>[States.Count];
            for (int l = 0; l < States.Count; l++)
            {
                TimeInStateSamples[l] = new List<double>();
            }
            TimesInStateCDFParams = new double[States.Count];


            CDFParams = new PiParams[Observations.Count, Observations.Count, States.Count];

        }

        //private void ComputeDencities()
        //{
        //    foreach (RTPObservation obs in Observations)
        //    {
        //        if (obs.ObservationNumber != 2)
        //        {
        //            foreach (RTPState state in States)
        //            {
        //                double[] ReceptionDurationSamples = ModelSamples.Where(s => s.State == state.StateNumber && s.Observation == obs.ObservationNumber).Select(s => s.Frame.ReceptionDuration).ToArray();
        //                for (int i = 0; i < ReceptionDurationSamples.Length; i++)
        //                    if (ReceptionDurationSamples[i] < 0.0000001) ReceptionDurationSamples[i] = 0.0000001;
        //                double[] res = GammaFit(ReceptionDurationSamples);
        //                Dencities[obs.ObservationNumber, state.StateNumber] = new GammaParams(res[0], res[1]);
        //                //StateDencities.Add(GammaFit(receiveSpeedSamples));
        //            }
        //        }
        //        else
        //        {
        //            double[] ReceptionDurationSamples = ModelSamples.Where(s => s.Observation == obs.ObservationNumber).Select(s => s.Frame.ReceptionDuration).ToArray();
        //            for (int i = 0; i < ReceptionDurationSamples.Length; i++)
        //                if (ReceptionDurationSamples[i] < 0.0000001) 
        //                    ReceptionDurationSamples[i] = 0.0000001;
        //            double[] res = GammaFit(ReceptionDurationSamples);
        //            foreach (RTPState state in States)
        //            {
        //                Dencities[obs.ObservationNumber, state.StateNumber] = new GammaParams(res[0], res[1]);
        //            }
        //        }
        //    }
        //}


        //        MLApp.MLApp matlab = new MLApp.MLApp(); 

        //// Change to the directory where the function is located 
        //matlab.Execute(@"cd c:\temp\example"); 

        //// Define the output 
        //object result = null; 

        //// Call the MATLAB function myfunc
        //matlab.Feval("myfunc", 2, out result, 3.14, 42.0, "world"); 



        private double[] GammaFit(double[] sequence)
        {
            object gamfit_result = null;
            Matlab.Feval("gamfit", 2, out gamfit_result, sequence);
            object[] res = gamfit_result as object[];
            double[] result = new double[2];
            result[0] = ((double[,])res[0])[0, 0];
            result[1] = ((double[,])res[0])[0, 1];
            return result;

            //MWNumericArray m_seq = new MWNumericArray(sequence);
            //MWArray res = matlabFunctions.GammaFit(m_seq);
            //double[] result = new double[2];
            //result[0] = ((double)(res.ToArray().GetValue(0, 0)));
            //result[1] = ((double)(res.ToArray().GetValue(0, 1)));
            //return result;
        }

        private double ExpFit(double[] sequence)
        {
            double result = 0;
            if (sequence.Count() > 0)
            {
                object exp_result = null;
                Matlab.Feval("expfit", 1, out exp_result, sequence);
                object[] res = exp_result as object[];
                result = ((double)res[0]);
            }
            return result;
            //MWNumericArray m_seq = new MWNumericArray(sequence);
            //MWArray res = matlabFunctions.ExpFit(m_seq);
            //double result = ((double)(res.ToArray().GetValue(0, 0)));
            //return result;

        }

        public static double GammaPDF(double t, GammaParams gammaParams)
        {
            //object gampdf_result = null;
            //Matlab.Feval("gampdf", 1, out gampdf_result, t, gammaParams.a, gammaParams.b);
            //object[] res = gampdf_result as object[];
            //double result = ((double)res[0]);
            //return result;

            return Gamma.PDF(gammaParams.a, 1/gammaParams.b,t);

            //MWNumericArray m_a = new MWNumericArray(gammaParams.a);
            //MWNumericArray m_b = new MWNumericArray(gammaParams.b);
            //MWNumericArray m_t = new MWNumericArray(t);

            //MWArray  res = matlabFunctions.GammaPDF(m_t, m_a, m_b);
            //double result = ((double)(res.ToArray().GetValue(0,0)));
            //return result;

        }

        public static double GammaCDF(double t, GammaParams gammaParams)
        {
            return Gamma.CDF(gammaParams.a, 1/gammaParams.b, t);
        }

        private double PhiHelper(double t, int j, int k, int l)
        {
            //object helper_result = null;
            double result = 0;

            PiParams piParams = CDFParams[j, k, l];
            if (piParams.ExpLambda > 0)
            {
                    double denominator = 1;
                    for (int i = 0; i < M; i++)
                    {
                        PiParams piParamsI = CDFParams[i, k, l];
                        if (piParamsI.ExpLambda > 0)
                        {
                            denominator += Exponential.CDF(piParamsI.ExpLambda,t) * piParamsI.Norm; //!!!!!!!!!!!!!!! + -> -
                        }
                    }

                    result = Exponential.PDF(piParams.ExpLambda,t) * piParams.Norm / denominator;

            }
            return result;
        }


        // PhiHelper GAMMA
        //private double PhiHelper(double t, int j, int k, int l)
        //{
        //    //object helper_result = null;
        //    double result = 0;

        //    PiParams piParams = CDFParams[j, k, l];
        //    if (piParams.Gamma.a > 0 && piParams.Gamma.b > 0)
        //    {
        //        if (t > 10)
        //        {
        //            result = 1;
        //            for (int i = 0; i < M; i++)
        //            {
        //                if (CDFParams[j, k, l].Gamma.b < CDFParams[i, k, l].Gamma.b) //если есть \theta_i > \theta_j, то результат = 0
        //                {
        //                    result = 0;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            double denominator = 1;
        //            for (int i = 0; i < M; i++)
        //            {
        //                PiParams piParamsI = CDFParams[i, k, l];
        //                if (piParamsI.Gamma.a > 0 && piParamsI.Gamma.b > 0)
        //                {
        //                    denominator += GammaCDF(t, piParamsI.Gamma) * piParamsI.Norm; //!!!!!!!!!!!!!!! + -> -
        //                }
        //            }

        //            result = GammaPDF(t, piParams.Gamma) * piParams.Norm / denominator;

        //            //NumberFormatInfo provider = new NumberFormatInfo();
        //            //provider.NumberDecimalSeparator = ".";
        //            //string command = string.Format(provider, "res = gampdf({0}, {1}, {2}) * {3} / (1 - 0", t, piParams.Gamma.a, piParams.Gamma.b, piParams.Norm);
        //            //for (int i = 0; i < M; i++)
        //            //{
        //            //    PiParams piParamsI = CDFParams[i, k, l];
        //            //    if (piParamsI.Gamma.a > 0 && piParamsI.Gamma.b > 0)
        //            //    {
        //            //         // + или - надо выяснить. Поставил +, чтобы проверить, будут ли бесконечние значения //command += string.Format(provider, " - gamcdf({0}, {1}, {2},'upper') * {3}", t, piParamsI.Gamma.a, piParamsI.Gamma.b, piParamsI.Norm);
        //            //        command += string.Format(provider, " + gamcdf({0}, {1}, {2},'upper') * {3}", t, piParamsI.Gamma.a, piParamsI.Gamma.b, piParamsI.Norm);
        //            //    }
        //            //}
        //            //command += ")";
        //            //Matlab.Execute(command);
        //            //object res = Matlab.GetVariable("res", "base");
        //            //result = (double)res;
        //        }
        //    }
        //return result;
        //}

        public void DefineStates(double RSM_lb, double RSM_ub)
        { 
            States.Add(new RTPState(0, 0, "Good", "Time to receive one packet is low", s => s.ReceiveSpeedMedian < RSM_lb));
            States.Add(new RTPState(1, 1, "Bad", "Time to receive one packet is high", s => s.ReceiveSpeedMedian >= RSM_lb && s.ReceiveSpeedMedian <= RSM_ub));
            States.Add(new RTPState(2, 2, "Ugly","Time to receive one packet is very high", s => s.ReceiveSpeedMedian > RSM_ub));
        }

        public void DefineObservations(int PC_b)
        {
            Observations.Add(new RTPObservation(0, 1, "Fat frames", "The number of packets in the frame is relatively high", s => s.PacketCount >= PC_b));
            Observations.Add(new RTPObservation(1, 2, "Slim frames", "The number of packets in the frame is relatively low", s => s.PacketCount < PC_b));
            Observations.Add(new RTPObservation(2, 0, "Loss", "The frame is not in time or there was a packet loss", s => !s.IsComplete || !s.IsInTime));
        }


        public void AddFrame(RTPFrame frame, List<Sample> Samples)
        {
            int stateNum = -1;
            int obsNum = -1;
            foreach (RTPState state in States.OrderBy(s => s.Priority))
            {
                if (state.Condition(frame))
                {
                    stateNum = state.StateNumber;
                    break;
                }
            }
            foreach (RTPObservation observation in Observations.OrderBy(s => s.Priority))
            {
                if (observation.Condition(frame))
                {
                    obsNum = observation.ObservationNumber;
                    break;
                }
            }
            Samples.Add(new Sample(frame.LastPacketReceptionTime, frame.LastPacketReceptionTime, stateNum, E[stateNum], obsNum, F[obsNum], frame));

        }


        public void AssignStatesAndObservations(RTPSession session, List<Sample> Samples)
        {
            foreach (RTPFrame frame in session.Frames)
            {
                AddFrame(frame, Samples);
            }     
        }

        public void TransitionMatrixEstimate()
        {

            ModelParamsInit();

            //double[,] 
            Lambda = MBuilder.Dense(States.Count, States.Count);
            double LastTimeInstant = 0;
            int LastState = -1;
            foreach (Sample s in ModelSamples)
            {
                if (s.Instant > 0)
                {
                    TimesInState[s.StateNumber] += s.Instant - LastTimeInstant;
                    if (s.StateNumber != LastState && LastState != -1)
                    {
                        TransitionCount[LastState, LastState] += 1;
                        TransitionCount[LastState, s.StateNumber] += 1;
                    }
                }
                LastTimeInstant = s.Instant;
                LastState = s.StateNumber;
            }
            for (int i = 0; i < States.Count; i++)
            {
                if (TimesInState[i] > 0)
                {
                    Lambda[i, i] = -1 / TimesInState[i];
                }
                else
                {
                    Lambda[i, i] = 0;
                }
            }
            for (int i = 0; i < States.Count; i++)
                for (int j = 0; j < States.Count; j++)
                {
                    if (i != j)
                    {
                        if (TransitionCount[i, i] > 0 || TransitionCount[i, j] > 0)
                        {
                            Lambda[i, j] = Math.Abs(Lambda[i, i]) * ((double)TransitionCount[i, j] / (double)TransitionCount[i, i]);
                        }
                        else
                        {
                            Lambda[i, j] = 0;
                        }
                    }

                }
            
            //return Lambda;
        }


        public void CDFParamsEstimate()
        {
            // i ->j, j->k, k->l
            //double[, ,] pi = new double[Observations.Count, Observations.Count, States.Count]; //условные вероятности переключения наблюдений при фиксированном состоянии  
            //Pi[j,k,l] = P{Yn = fj | Yn-1 = fk, Xn-1 = el} // P{Yn = fi | Yn-1 = fj, Xn-1 = ek} 

            PiParams[,,] result = new PiParams[Observations.Count, Observations.Count, States.Count]; //условные вероятности переключения наблюдений при фиксированном состоянии  

            int[,,] N = new int[Observations.Count, Observations.Count, States.Count]; //количество соотвествующих сэмплов


            int prevState = -1;
            int prevObs = -1;
            double previousStateChangeInstant = 0;
            foreach (Sample sample in ModelSamples) //бежим по сэмплам
            {
                if (prevState >= 0 && prevObs >= 0)
                {
                    N[sample.ObservationNumber, prevObs, prevState]++;
                    if (sample.Frame.ReceptionDuration > 0.0000001)
                        GammaSamples[sample.ObservationNumber, prevObs, prevState].Add(sample.Frame.ReceptionDuration);
                    else
                        GammaSamples[sample.ObservationNumber, prevObs, prevState].Add(0.0000001);

                    if (sample.StateNumber != prevState)
                    {
                        TimeInStateSamples[prevState].Add(sample.Instant - previousStateChangeInstant);
                        previousStateChangeInstant = sample.Instant;
                    }
                }
                prevState = sample.StateNumber;
                prevObs = sample.ObservationNumber;
            }


            for (int l = 0; l < States.Count; l++)
            {
                for (int k = 0; k < Observations.Count; k++)
                {
                    int sum = 0;
                    for (int j = 0; j < Observations.Count; j++)
                    {
                        sum += N[j, k, l];
                    }
                    for (int j = 0; j < Observations.Count; j++)
                    {
                        double norm = 0;
                        if (sum > 0) // если 0, то очень фигово оценивается распределение
                        {
                            norm = (double)N[j, k, l] / (double)sum;
                        }
                        else
                        {
                            norm = 0;
                        }

                        double[] res = { 0, 0 };
                        double lambdares = 0;
                        if (GammaSamples[j, k, l].Count > 0) // если 0, то очень фигово оценивается распределение при Count = 1 или 2
                        {
                            res = GammaFit(GammaSamples[j, k, l].ToArray());
                            lambdares = ExpFit(GammaSamples[j, k, l].ToArray());
                            //result[j, k, l].Gamma = new GammaParams(res[0], res[1]);
                        }
                        result[j, k, l] = new PiParams(norm, new GammaParams(res[0], res[1]));
                        result[j, k, l].ExpLambda = 1/lambdares;
                    }
                }
                TimesInStateCDFParams[l] = ExpFit(TimeInStateSamples[l].ToArray());
            }
            CDFParams = result;
        }

        public void ExportGammaData(string folderOut)
        {
            for (int l = 0; l < States.Count; l++)
                for (int k = 0; k < Observations.Count; k++)
                {
                    for (int j = 0; j < Observations.Count; j++)
                    {
                        if (GammaSamples[l, j, k].Count > 0)
                        {
                            string subdirname = l.ToString() + "_" + k.ToString();
                            DirectoryInfo dir = new DirectoryInfo(folderOut); 
                            dir.CreateSubdirectory(subdirname);
                            NumberFormatInfo provider = new NumberFormatInfo();
                            provider.NumberDecimalSeparator = ".";
                            string fileOut = String.Format("{0}\\{1}\\{2}_{3}_{4}_gamma_data.txt", folderOut, subdirname, l, j, k);
                            System.IO.StreamWriter outputfile = new System.IO.StreamWriter(fileOut, true);
                            foreach (double gs in GammaSamples[l, j, k])
                            {
                                outputfile.WriteLine(String.Format(provider, "{0}", gs));
                            }
                            outputfile.Close();

                            fileOut = String.Format("{0}\\{1}\\{2}_{3}_{4}_gamma_params.txt", folderOut, subdirname, l, j, k);
                            outputfile = new System.IO.StreamWriter(fileOut, true);
                            outputfile.WriteLine(String.Format(provider, "{0} {1}", CDFParams[l, j, k].Gamma.a, CDFParams[l, j, k].Gamma.b));
                            outputfile.Close();
                        }
                    }
                }
        }


        public void ExportGammaParams(string folderOut, double time)
        {
            for (int l = 0; l < States.Count; l++)
                for (int k = 0; k < Observations.Count; k++)
                {
                    for (int j = 0; j < Observations.Count; j++)
                    {
                        if (GammaSamples[l, j, k].Count > 0)
                        {
                            string subdirname = l.ToString() + "_" + k.ToString();
                            DirectoryInfo dir = new DirectoryInfo(folderOut);
                            dir.CreateSubdirectory(subdirname);
                            NumberFormatInfo provider = new NumberFormatInfo();
                            provider.NumberDecimalSeparator = ".";
                            
                            string fileOut = String.Format("{0}\\{1}\\{2}_{3}_{4}_gamma_params.txt", folderOut, subdirname, l, j, k);
                            System.IO.StreamWriter outputfile = new System.IO.StreamWriter(fileOut, true);
                            if (!double.IsInfinity(CDFParams[l, j, k].Gamma.a) && !double.IsInfinity(CDFParams[l, j, k].Gamma.b))
                            {
                                outputfile.WriteLine(String.Format(provider, "{0} {1} {2}", time, CDFParams[l, j, k].Gamma.a, CDFParams[l, j, k].Gamma.b));
                            }
                            outputfile.Close();
                        }
                    }
                }
        }

        public void ExportTimesInStateData(string folderOut)
        {
            for (int l = 0; l < States.Count; l++)
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                string fileOut = String.Format("{0}\\{1}_timeinstate_data.txt", folderOut, l);
                System.IO.StreamWriter outputfile = new System.IO.StreamWriter(fileOut, true);
                foreach (double gs in TimeInStateSamples[l])
                {
                    outputfile.WriteLine(String.Format(provider, "{0}", gs));
                }
                outputfile.Close();

                fileOut = String.Format("{0}\\{1}_timeinstate_params.txt", folderOut, l);
                outputfile = new System.IO.StreamWriter(fileOut, true);
                outputfile.WriteLine(String.Format(provider, "{0}", TimesInStateCDFParams[l]));
                outputfile.Close();
            }
        }

        public void Process()
        {
            double start = SystemSamples.Min(s => s.Instant);
            //double start = SystemSamples[lastProcessedSystemSample].Instant;
            double currentTime = start;

            List<Sample> SortedSamples = SystemSamples.OrderBy(s => s.Instant).ToList(); // нужно отсортировать по времени прихода, иначе будет фигня с отрицательными временными интервалами
            SystemSamples = SortedSamples; 

            for (int i = 0; i < SystemSamples.Count - 1; i++)
            {
                if (i > 0)
                {
                    EstimateSamples.Add(new Sample(SystemSamples[i].Instant, SystemSamples[i - 1].Instant, SystemSamples[i].StateNumber, SystemSamples[i].State, SystemSamples[i - 1].ObservationNumber, SystemSamples[i - 1].Observation, SystemSamples[i].Frame));
                }
                else
                {
                    EstimateSamples.Add(SystemSamples[i]);
                }

                while (currentTime + step < SystemSamples[i + 1].Instant)
                {
                    currentTime += step;
                    EstimateSamples.Add(new Sample(currentTime, SystemSamples[i].Instant, SystemSamples[i].StateNumber, SystemSamples[i].State, SystemSamples[i].ObservationNumber, SystemSamples[i].Observation, null));
                }
                //lastProcessedSystemSample = i;
            }

        }

        public void Estimate()
        {

            //if (estimationStartInstance > 0)
            //{
                //Matrix<double> XhatS_ = p0();
            //double S_ = estimationStartInstance;
            //double S_ = 0;
            //if (lastEstimatedSample == 0)
            //    {
            //        lastEstimatedSample = EstimateSamples.FindIndex(s => s.Instant == estimationStartInstance);
            //        if (lastEstimatedSample == -1)
            //        {
            //            lastEstimatedSample = EstimateSamples.Count;
            //        }
            //        EstimateSamples[lastEstimatedSample - 1].XHat = p0();
            //    }
            Matrix<double> XhatS_ = p0();
            double S_ = 0;

            for (int i = 0; i < EstimateSamples.Count - 1; i++)
            {
                if (i == 0)
                {
                    EstimateSamples[i].XHat = p0();
                }
                else
                {
                    XhatS_ = EstimateSamples[i - 1].XHat;
                    S_ = EstimateSamples[i - 1].Instant;
                    EstimateSamples[i].XHat = XHat(XhatS_, EstimateSamples[i].Observation, EstimateSamples[i].Instant, EstimateSamples[i].Instant - S_, EstimateSamples[i].PreviousObservationInstant);
                    if (EstimateSamples[i].Frame != null)
                    {
                        EstimateSamples[i].XHat = XHatJump(EstimateSamples[i].XHat, EstimateSamples[i].Observation, EstimateSamples[i].Instant, EstimateSamples[i].PreviousObservationInstant);
                    }
                }
                //bool fail = false;
                //fail = fail || Double.IsNaN(EstimateSamples[i].XHat[0, 0]);
                //fail = fail || Double.IsNaN(EstimateSamples[i].XHat[1, 0]);
                //fail = fail || Double.IsNaN(EstimateSamples[i].XHat[2, 0]);
                //fail = fail || Double.IsNegativeInfinity(EstimateSamples[i].XHat[0, 0]);
                //fail = fail || Double.IsNegativeInfinity(EstimateSamples[i].XHat[1, 0]);
                //fail = fail || Double.IsNegativeInfinity(EstimateSamples[i].XHat[2, 0]);
                //fail = fail || Double.IsPositiveInfinity(EstimateSamples[i].XHat[0, 0]);
                //fail = fail || Double.IsPositiveInfinity(EstimateSamples[i].XHat[1, 0]);
                //fail = fail || Double.IsPositiveInfinity(EstimateSamples[i].XHat[2, 0]);
                //if (EstimateSamples[i].XHat[0, 0] < 0) fail = true;
                //if (EstimateSamples[i].XHat[1, 0] < 0) fail = true;
                //if (EstimateSamples[i].XHat[2, 0] < 0) fail = true;
                //if (!fail)
                //{
                //    lastEstimatedSample++;
                //}
                //else
                //{
                //    lastEstimatedSample = 0;
                //    foreach (Sample s in EstimateSamples)
                //    {
                //        s.XHat = null;
                //    }
                //    estimationStartInstance = 0;
                //    break;
                //}
            }
            //}

            //foreach (Sample s in EstimateSamples.Where(s => s.XHat == null))
            //{
            //    if (stepNumber == 0)
            //    {
            //        s.XHat = p0();
            //    }
            //    else
            //    {
            //        s.XHat = XHat(XhatS_, s.Observation, s.Instant, s.Instant - S_, s.PreviousObservationInstant);
            //        if (s.Frame != null)
            //        {
            //            s.XHat = XHatJump(s.XHat, s.Observation, s.Instant, s.PreviousObservationInstant);
            //        }
            //        XhatS_ = s.XHat;
            //    }
            //    S_ = s.Instant;
            //    stepNumber++;
            //}

        }

        public bool EstimateNext(int SampleNumber)
        {
            //Matrix<double> XhatS _ = p0();
            double S_ = 0;
            int i = SampleNumber;
            bool res = true;
            if (Lambda != null)
            {
                if (i == 0)
                {
                    EstimateSamples[i].XHat = p0();
                }
                else
                {
                    if (EstimateSamples[i - 1].XHat == null)
                    {
                        EstimateSamples[i].XHat = p0();
                    }
                    else
                    {
                        Matrix<double> XhatS_ = EstimateSamples[i - 1].XHat;
                        S_ = EstimateSamples[i - 1].Instant;
                        EstimateSamples[i].XHat = XHat(XhatS_, EstimateSamples[i].Observation, EstimateSamples[i].Instant, EstimateSamples[i].Instant - S_, EstimateSamples[i].PreviousObservationInstant);
                        if (EstimateSamples[i].Frame != null)
                        {
                            EstimateSamples[i].XHat = XHatJump(EstimateSamples[i].XHat, EstimateSamples[i].Observation, EstimateSamples[i].Instant, EstimateSamples[i].PreviousObservationInstant);
                        }
                        if (double.IsNaN(EstimateSamples[i].XHat[0, 0]))
                        {
                            EstimateSamples[i].XHat = null;
                            //S_ = 0; //debug
                            res = false;
                        }
                    }
                }
            }
            return res;
        }
        


        public void SaveEstimate(string folderOut)
        {
            DirectoryInfo dir = new DirectoryInfo(folderOut);
            int fileNum = dir.EnumerateFiles("Estimate*.*").Count();
            string fileOut = folderOut + "\\Estimate_" + fileNum.ToString() + ".txt";

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            //System.IO.StreamWriter outputfile_ = new System.IO.StreamWriter("Estimate.txt");
            //outputfile_.Close();

            System.IO.StreamWriter outputfile = new System.IO.StreamWriter(fileOut, true);
            foreach (Sample s in EstimateSamples)
            {
                if (s.XHat != null)
                {
                    outputfile.WriteLine(String.Format(provider, "{0} {1} {2} {3} {4} {5}", s.Instant, s.XHat[0, 0], s.XHat[1, 0], s.XHat[2, 0], s.StateNumber, s.ObservationNumber));
                }
                else
                {
                    outputfile.WriteLine(String.Format(provider, "{0} {1} {2} {3} {4} {5}", s.Instant, -1, -1, -1, s.StateNumber, s.ObservationNumber));
                }
            }
            outputfile.Close();

        }

        private double Pi(int j, int k, int l, double t)
        {
            double result = 0;
            if (CDFParams[j, k, l].Gamma.a > 0 && CDFParams[j, k, l].Gamma.b > 0)
            {
                result = GammaPDF(t, CDFParams[j, k, l].Gamma) * CDFParams[j, k, l].Norm;
            }
            return result;
        }

        //private Matrix<double> Y(double t)
        //{
        //    Matrix<double> result;

        //    double lastInstant = SystemSamples.Where(s => s.Instant < t).Max(s => s.Instant);
        //    result = SystemSamples.First(s => s.Instant == lastInstant).Observation;
            
        //    return result;
        //}

        //private double tau(double t)
        //{
        //    double result = SystemSamples.Where(s => s.Instant < t).Max(s => s.Instant);
        //    return result;
        //}

        //private double varphi(int j, int l, double t)
        //{
        //    double result = 0;
        //    for (int k = 0; k < M; k++)
        //    {
        //        Matrix<double> Prod = F[k].Transpose() * Y(t);
        //        if (Prod[0,0] > 0)
        //        {
        //            //result += Prod[0, 0] * Pi(j, k, l, t - tau(t)) / (1 - Integrate.OnClosedInterval(u => Pi(j, k, l, u), 0, t - tau(t), 0.001));
        //            result += Prod[0, 0] * PhiHelper(t - tau(t), j, k, l);
        //                //Pi(j, k, l, t - tau(t)) / (1 - Integrate.OnClosedInterval(u => Pi(j, k, l, u), 0, t - tau(t), 0.001));
        //        }
        //    }
        //    return result;

        //}

        private double varphi(int j, int l, double t, Matrix<double> Y, double tau)
        {
            double result = 0;
            for (int k = 0; k < M; k++)
            {
                Matrix<double> Prod = F[k].Transpose() * Y;
                if (Prod[0, 0] > 0)
                {
                    //result += Prod[0, 0] * Pi(j, k, l, t - tau(t)) / (1 - Integrate.OnClosedInterval(u => Pi(j, k, l, u), 0, t - tau(t), 0.001));
                    result += Prod[0, 0] * PhiHelper(t - tau, j, k, l);
                    //Pi(j, k, l, t - tau(t)) / (1 - Integrate.OnClosedInterval(u => Pi(j, k, l, u), 0, t - tau(t), 0.001));
                }
            }
            if (double.IsNaN(result)) result = 0; ///// ?????
            return result;

        }

        //private Matrix<double> varphi(double t)
        //{
        //    Matrix<double> result = MBuilder.Dense (M, N, (j, l) => varphi(j, l, t));
        //    return result;
        //}

        private Matrix<double> varphi(double t, Matrix<double> Y, double tau)
        {
            Matrix<double> result = MBuilder.Dense(M, N, (j, l) => varphi(j, l, t, Y, tau));
            return result;
        }

        private Matrix<double> Diag(Matrix<double> V)
        {
            return DenseMatrix.OfDiagonalVector(V.Column(0));
        }

        private Matrix<double> Normalize(Matrix<double> V)
        {
            return V.Column(0).Normalize(1).ToColumnMatrix();
        }

        //private Matrix<double> XHat(Matrix<double> X, double t, double deltat)
        //{
        //    //Vector<double> Xhat = X;
        //    //Matrix<double> Prognosis = DenseMatrix.OfArray(Lambda).TransposeThisAndMultiply(X.ToColumnMatrix());
        //    Matrix<double> VarPhi = varphi(t);
        //    Matrix<double> Prognosis = Lambda.Transpose() * X;
        //    Matrix<double> K = Diag(X) - X * X.Transpose();
        //    //Matrix<double> Inv = Diag(VarPhi * X).Inverse();
        //    Matrix<double> Ones = MBuilder.Dense(M, 1, 1.0);
        //    //Matrix<double> result = Prognosis - K * VarPhi.Transpose() * Inv * VarPhi * X;
        //    Matrix<double> result = Prognosis - K * VarPhi.Transpose() * Ones;
        //    Matrix<double> Xhat = Normalize(X + deltat * result);
        //    return Xhat;
        //}

        private Matrix<double> XHat(Matrix<double> X, Matrix<double> Y, double t, double deltat, double tau)
        {
            //Vector<double> Xhat = X;
            //Matrix<double> Prognosis = DenseMatrix.OfArray(Lambda).TransposeThisAndMultiply(X.ToColumnMatrix());
            Matrix<double> VarPhi = varphi(t, Y, tau);
            Matrix<double> Prognosis = Lambda.Transpose() * X;
            Matrix<double> K = Diag(X) - X * X.Transpose();
            //Matrix<double> Inv = Diag(VarPhi * X).Inverse();
            Matrix<double> Ones = MBuilder.Dense(M, 1, 1.0);
            //Matrix<double> result = Prognosis - K * VarPhi.Transpose() * Inv * VarPhi * X;
            Matrix<double> result = Prognosis - K * VarPhi.Transpose() * Ones;
            Matrix<double> Xhat = Normalize(X + deltat * result);
            return Xhat;
        }


        //private Matrix<double> XHatAndJump(Matrix<double> X, double t, double deltat)
        //{
        //    //Vector<double> Xhat = X;
        //    //Matrix<double> Prognosis = DenseMatrix.OfArray(Lambda).TransposeThisAndMultiply(X.ToColumnMatrix());
        //    Matrix<double> VarPhi = varphi(t);
        //    Matrix<double> Prognosis = Lambda.Transpose() * X;
        //    Matrix<double> K = Diag(X) - X * X.Transpose();
        //    Matrix<double> Inv = Diag(VarPhi * X).Inverse();
        //    Matrix<double> result = Prognosis - K * VarPhi.Transpose() * Inv * VarPhi * X;
        //    Matrix<double> Xhat = Normalize(X + deltat * result);
        //    Xhat = Normalize(Xhat + K * VarPhi.Transpose() * Inv * Y(t));
        //    return Xhat;
        //}



        //private Matrix<double> XHatJump(Matrix<double> X, double t)
        //{
        //    //Vector<double> Xhat = X;
        //    Matrix<double> Xhat = X;
        //    Matrix<double> VarPhi = varphi(t);

        //    if ((Y(t).Transpose() * VarPhi * X)[0, 0] > 0.0000001)
        //    {

        //        Matrix<double> K = Diag(X) - X * X.Transpose();
        //        Matrix<double> Inv = Diag(VarPhi * X).Inverse();
        //        Matrix<double> result = X + K * VarPhi.Transpose() * Inv * Y(t);
        //        Xhat = Normalize(result);
        //    }
        //    return Xhat;
        //}

        private Matrix<double> XHatJump(Matrix<double> X, Matrix<double> Y, double t, double tau)
        {
            //Vector<double> Xhat = X;
            Matrix<double> Xhat = X;
            Matrix<double> VarPhi = varphi(t, Y, tau);

            if ((Y.Transpose() * VarPhi * X)[0, 0] > 0.0000001)
            {
                if (Diag(VarPhi * X)[0, 0] > 0.00000001 && Diag(VarPhi * X)[1, 1] > 0.00000001 && Diag(VarPhi * X)[2, 2] > 0.00000001)
                {
                    Matrix<double> K = Diag(X) - X * X.Transpose();
                    Matrix<double> Inv = Diag(VarPhi * X).Inverse();
                    Matrix<double> result = X + K * VarPhi.Transpose() * Inv * Y;
                    Xhat = Normalize(result);
                }
            }
            return Xhat;
        }

        public Matrix<double> p0()
        {
            double th = 0.000000001;
            Matrix<double> L = Lambda;
            L.CoerceZero(th);
            Vector<double> Lcs = L.ColumnSums();
            int nonZeroLcsCount = 0;
            int lastNonZero = -1;
            for (int i = 0; i < Lcs.Count(); i++)
            {
                if (Math.Abs(Lcs[i]) < th)
                {
                    L[i, i] = 1.0;
                }
                else
                {
                    nonZeroLcsCount++;
                    lastNonZero = i;
                }
            }
            if (nonZeroLcsCount == 1)
            {
                return VBuilder.Dense(N, i => i == lastNonZero ? 1 : 0).ToColumnMatrix();
            }
            else
            {
                Matrix<double> A = MBuilder.Dense(1, N, 1.0);
                A = A.Stack(Lambda.Transpose());
                Vector<double> B = VBuilder.Dense(N + 1, i => i > 0 ? 0 : 1);
                Vector<double> result = A.Solve(B);
                return result.ToColumnMatrix();
            }
        }

        public void SaveToMatlab(string path)
        {
            System.IO.StreamWriter outputMfile = new System.IO.StreamWriter(path);
            outputMfile.Write(ExportToMatlabString().ToString());
            outputMfile.Close();
        }

        public StringBuilder ExportToMatlabString()
        {
            StringBuilder result = new StringBuilder();

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            result.AppendLine("% матрица интенсивностей");
            result.AppendLine(ExportMatrix("Lambda = ", ";", (i, j) => Lambda[i, j], States.Count, States.Count));
            result.AppendLine("");

            result.AppendLine("% параметры Гамма-распределений");
            //result.AppendLine(ExportMatrix("a = ", ";", (i, j) => Dencities[i, j].a, Observations.Count, States.Count));
            //result.AppendLine(ExportMatrix("b = ", ";", (i, j) => Dencities[i, j].b, Observations.Count, States.Count));
            result.AppendLine("");

            result.AppendLine("% условные вероятности");
            for (int k = 0; k < States.Count; k++)
            {
              //  result.AppendLine(ExportMatrix(string.Format(provider, "Pi(:,:,{0}) = ", k + 1), ";", (i, j) => Pi[i, j, k], States.Count, States.Count));
            }
            result.AppendLine("");

            return result;
        
        }

        public string ExportMatrix(string prefix, string postfix, Func<int, int, double> table, int cols, int rows)
        {
            StringBuilder result = new StringBuilder();
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            //result.AppendLine("% матрица интенсивностей");
            result.Append(prefix);
            result.Append("[");

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    result.Append(string.Format(provider, "{0}", table(i,j)));
                    if (j < rows - 1)
                        result.Append(", ");
                }
                if (i < cols - 1)
                    result.Append("; ");
            }
            result.Append("]");
            result.Append(postfix);
            return result.ToString();
        }
    }

}
