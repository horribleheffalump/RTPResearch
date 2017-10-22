import matplotlib
import matplotlib.pyplot as plt
import numpy as np
import scipy.io as io
import matplotlib.tri as mtri
matplotlib.rc('text', usetex = True)
import pylab
#import OutputParcerFunction




##filename = "../Data/Linphone_3g_20min_720p_2/Linphone_3g_20min_720p_2_540801797_(91.121.209.194-to-10.4.44.101)_V_frames.txt"
##filename = u"../Data/Linphone_3g_20min_720p_4/Linphone_3g_20min_720p_4_1394698631_(213.87.130.168-to-10.4.44.101)_V_frames.txt"
#filename = u"../Data/data 2015-12-3 18-9/frames.txt"
#Number, PacketCount, PacketCountMedian, PacketCountAverage, TimeStampSender, LastPacketReceptionTime, ReceptionDuration, IsComplete, MarkedPacketReceived, AreThereOutOfOrder, ReceiveSpeed, ReceiveSpeedMedian, IsInTime = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5,6,7,8,9,10,11,12), unpack=True, dtype=float)
##filename = "../Data/Linphone_3g_20min_720p_2/Estimate_1.txt" #Estimate_1 0.01, Estimate_2 0.001  
##filename = u"../Data/Linphone_3g_20min_720p_4/Estimate_9.txt" #Estimate_2 0.01, Estimate_3 0.001  
#filename = u"../Data/data 2015-12-3 18-9/Estimate_0.txt"
#t, Prob1, Prob2, Prob3, E, F = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5), unpack=True, dtype=float)




#filename = u"../Data/data 2015-12-10 16-0 - 3g 20min - online estimate - 0.04 lb/frames.txt"
#filename = u"../Data/data 2015-12-10 18-36 - 3g 20min - online estimate - 0.04 lb/frames.txt"
filename = u"../Data/data 2015-12-11 18-42 - 3g 20min - online estimate - 0.04 lb - exp/frames.txt"
Number, PacketCount, PacketCountMedian, PacketCountAverage, TimeStampSender, LastPacketReceptionTime, ReceptionDuration, IsComplete, MarkedPacketReceived, AreThereOutOfOrder, ReceiveSpeed, ReceiveSpeedMedian, IsInTime = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5,6,7,8,9,10,11,12), unpack=True, dtype=float)

#filename = u"../Data/data 2015-12-10 16-0 - 3g 20min - online estimate - 0.04 lb/Estimate_0.txt"
filename = u"../Data/data 2015-12-10 18-36 - 3g 20min - online estimate - 0.04 lb/Estimate_0.txt"
t, Prob1, Prob2, Prob3, E, F = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5), unpack=True, dtype=float)


filename = u"../Data/data 2015-12-11 18-42 - 3g 20min - online estimate - 0.04 lb - exp/Estimate_0.txt"
tExp, Prob1Exp, Prob2Exp, Prob3Exp = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3), unpack=True, dtype=float)

E1 = np.zeros(len(t));
E2 = np.zeros(len(t));
E3 = np.zeros(len(t));

for i in range(0, len(t)-1):
        if (E[i] == 0): 
            E1[i] = 1.0
        if (E[i] == 1): 
            E2[i] = 1.0
        if (E[i] == 2): 
            E3[i] = 1.0       


tshort = [];
P1 = [];
P2 = [];
P3 = [];



time_in_err = 0
for i in range(1, len(t)-1):
    if (Prob1[i] + Prob2[i] + Prob3[i] > 0):
        maxP = max(Prob1[i], Prob2[i], Prob3[i])
        Eest1 = 0
        Eest2 = 0
        Eest3 = 0
        if (maxP == Prob1[i]): Eest1 = 1
        if (maxP == Prob2[i]): Eest2 = 1
        if (maxP == Prob3[i]): Eest3 = 1
        err = 0
        if (Eest1 != E1[i] or Eest2 != E2[i] or Eest3 != E3[i]): err = 1
        if (err > 0): time_in_err += (t[i] - t[i-1])

m_err = time_in_err / (t[len(t)-1] - t[0])



time_in_err_exp = 0
for i in range(1, len(tExp)-1):
    if (Prob1Exp[i] + Prob2Exp[i] + Prob3Exp[i] > 0):
        maxP = max(Prob1Exp[i], Prob2Exp[i], Prob3Exp[i])
        Eest1 = 0
        Eest2 = 0
        Eest3 = 0
        if (maxP == Prob1Exp[i]): Eest1 = 1
        if (maxP == Prob2Exp[i]): Eest2 = 1
        if (maxP == Prob3Exp[i]): Eest3 = 1
        err = 0
        if (Eest1 != E1[i] or Eest2 != E2[i] or Eest3 != E3[i]): err = 1
        if (err > 0): time_in_err_exp += (t[i] - t[i-1])

m_err_exp = time_in_err_exp / (tExp[len(t)-1] - tExp[0])


disp_gamma = 0;
disp_exp = 0;
disp_process = 0;
n_gamma = 0;
n_exp = 0;

for i in range(1, len(t)-1):
    if (Prob1[i] + Prob2[i] + Prob3[i] > 0):
        n_gamma += 1
        disp_process += (pow(E1[i],2) + pow(E2[i],2) + pow(E3[i],2)) * (t[i] - t[i-1])
        disp_gamma += (pow(E1[i]  - Prob1[i],2) + pow(E2[i] - Prob2[i],2)+ pow(E3[i] - Prob3[i],2)) * (t[i] - t[i-1])

disp_process = disp_process / (t[len(t)-1] - t[0])
disp_gamma = disp_gamma / (t[len(t)-1] - t[0])
        
for i in range(0, len(tExp)-1):
    if (Prob1Exp[i] + Prob2Exp[i] + Prob3Exp[i] > 0):
        n_exp += 1
        disp_exp += (pow(E1[i]  - Prob1Exp[i],2) + pow(E2[i] - Prob2Exp[i],2)+pow(E3[i] - Prob3Exp[i],2)) *  (tExp[i] - tExp[i-1])

disp_exp = disp_exp / (tExp[len(tExp)-1] - tExp[0])


#print(disp_process)
#print(pow(disp_exp,0.5))
#print(pow(disp_gamma,0.5))

print(m_err)
print(m_err_exp)