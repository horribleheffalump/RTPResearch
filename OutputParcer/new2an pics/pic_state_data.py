﻿import matplotlib
import matplotlib.pyplot as plt
import numpy as np
import scipy.io as io
import matplotlib.tri as mtri
matplotlib.rc('text', usetex = True)
import pylab
import OutputParcerFunction

rcv_spd_lb = 0.04
rcv_spd_ub = 0.08

##filename = "../Data/Linphone_3g_20min_720p_2/Linphone_3g_20min_720p_2_540801797_(91.121.209.194-to-10.4.44.101)_V_frames.txt"
#filename = u"../Data/Linphone_3g_20min_720p_4/Linphone_3g_20min_720p_4_1394698631_(213.87.130.168-to-10.4.44.101)_V_frames.txt"
#Number, PacketCount, PacketCountMedian, PacketCountAverage, TimeStampSender, LastPacketReceptionTime, ReceptionDuration, IsComplete, MarkedPacketReceived, AreThereOutOfOrder, ReceiveSpeed, ReceiveSpeedMedian, State, IsInTime = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5,6,7,8,9,10,11,12,13), unpack=True, dtype=float)

##filename = "../Data/Linphone_3g_20min_720p_2/Estimate_1.txt" #Estimate_1 0.01, Estimate_2 0.001  
#filename = u"../Data/Linphone_3g_20min_720p_4/Estimate_5.txt" #Estimate_2 0.01, Estimate_3 0.001  
#t, Prob1, Prob2, Prob3, E, F = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5), unpack=True, dtype=float)


filename = u"../Data/data 2015-12-9 10-57 - 3g 20min - estimate by the  first quater - 0.04 lb/frames.txt"
Number, PacketCount, PacketCountMedian, PacketCountAverage, TimeStampSender, LastPacketReceptionTime, ReceptionDuration, IsComplete, MarkedPacketReceived, AreThereOutOfOrder, ReceiveSpeed, ReceiveSpeedMedian, IsInTime = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5,6,7,8,9,10,11,12), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-9 10-57 - 3g 20min - estimate by the  first quater - 0.04 lb/Estimate_0.txt"
t, Prob1, Prob2, Prob3, E, F = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5), unpack=True, dtype=float)


IsIncomplete = []#np.ndarray((1,1), dtype = float); 
TIsIncomplete = []#np.ndarray((1,1), dtype = float); 
IsNotInTime = []#np.ndarray((1,1), dtype = float); 
TIsNotInTime = []#np.ndarray((1,1), dtype = float); 


for i in range(0, len(Number)-1):
    if (IsComplete[i] == 0):
        IsIncomplete.append(-0.02)
        TIsIncomplete.append(LastPacketReceptionTime[i]) 
    if (IsInTime[i] == 0):
        IsNotInTime.append(-0.01)
        TIsNotInTime.append(LastPacketReceptionTime[i]) 

#    IsIncomplete[i] = 1 - IsComplete[i];
#    IsNotInTime[i] = 1 - IsInTime[i];



from pylab import *

f = plt.figure(num=None, figsize=(10, 4.5), dpi=150, facecolor='w', edgecolor='k')
subplots_adjust(hspace=0.0)

plt.subplots_adjust(left=0.04, bottom=0.07, right=0.97, top=0.95, wspace=0.1)

o = np.zeros(len(Prob1))
ones = np.ones(len(Prob1))
levelzero = np.ones(len(Prob1))*-0.06
levelone = np.ones(len(Prob1))*-0.05
leveltwo = np.ones(len(Prob1))*-0.04
levelthree = np.ones(len(Prob1))*-0.03



ax1 = subplot(111)
ax1.plot(LastPacketReceptionTime, ReceiveSpeed,color='grey')
ax1.plot(LastPacketReceptionTime, ReceiveSpeedMedian, color='black', linewidth=2.0)
ax1.plot(TIsNotInTime, IsNotInTime, 'k>')
ax1.plot(TIsIncomplete, IsIncomplete, 'kx')
ax1.fill_between(t, leveltwo, levelthree, where=E==o, color='black', alpha = 0.2, linewidth=0.0);
ax1.fill_between(t, levelone, leveltwo, where=E==ones, color='black', alpha = 0.4, linewidth=0.0);
ax1.fill_between(t, levelzero, levelone, where=E==ones*2, color='black', alpha = 0.8, linewidth=0.0);
ax1.plot(t, rcv_spd_lb*ones, 'k--', linewidth=0.75)
ax1.plot(t, rcv_spd_ub*ones, 'k--', linewidth=0.75)
ax1.set_yticks([0,0.1,0.2])
ax1.set_ylim(-0.07,0.3)

ax2 = ax1.twinx()
ax2.plot(LastPacketReceptionTime, PacketCount, '-', color="black", alpha = 0.8)
ax2.set_ylim(-20,18)
ax2.set_yticks([5,10,15])
#o = np.zeros(len(Prob1))
#ones = np.ones(len(Prob1))
#up = np.ones(len(Prob1))*ub
#low = np.ones(len(Prob1))*lb
#ax1 = subplot(311)
#ax1.plot(t, Prob1,color='black')
#ax1.fill_between(t, low, up, where=E==o, color='grey', alpha = 0.2, linewidth=0.0);
#ax1.set_yticks([0,1])
#ax1.set_ylim(lb,ub)

#ax2 = subplot(312, sharex=ax1)
#ax2.plot(t,Prob2,color='black')
#ax2.fill_between(t, low, up, where=E==ones, color='grey', alpha = 0.4, linewidth=0.0);
#ax2.set_yticks([0,1])
#ax2.set_ylim(lb,ub)


#ax3 = subplot(313, sharex=ax1)
#ax3.plot(t,Prob3,color='black')
#ax3.fill_between(t, low, up, where=E==ones*2, color='grey', alpha = 0.8, linewidth=0.0);
#ax3.set_yticks([0,1])
#ax3.set_ylim(lb,ub)

#xticklabels = ax1.get_xticklabels()+ax2.get_xticklabels()
#setp(xticklabels, visible=False)


#ax1.spines['right'].set_visible(False)
ax1.spines['top'].set_visible(False)
#ax1.spines['bottom'].set_visible(False)
#ax1.yaxis.set_ticks_position('left')
#ax1.xaxis.set_ticks_position('none')
#ax2.spines['right'].set_visible(False)
#ax2.spines['top'].set_visible(False)
#ax2.spines['bottom'].set_visible(False)
#ax2.yaxis.set_ticks_position('left')
#ax2.xaxis.set_ticks_position('none')
#ax3.spines['right'].set_visible(False)
#ax3.spines['top'].set_visible(False)
#ax3.yaxis.set_ticks_position('left')
#ax3.xaxis.set_ticks_position('bottom')

ax1.set_xlim(-50,LastPacketReceptionTime[len(LastPacketReceptionTime)-1] + 50);
ax1.set_xticks([0,500,1000]);

#savefilename = u"D:/Наука/_Статьи/__в работе/2015 - BB-LNCS/pic5_state_data.pdf";
#f.savefig(savefilename)
show()
