import matplotlib
import matplotlib.pyplot as plt
import numpy as np
import scipy.io as io
import matplotlib.tri as mtri
matplotlib.rc('text', usetex = True)
import pylab
import OutputParcerFunction



#filename = "D:/?????/_??????/__? ??????/2015 - BB-LNCS/MSNetworkMonitor/CAPTools/x64/Debug/Linphone_3g_20min_720p_2/Linphone_3g_20min_720p_2_540801797_(91.121.209.194-to-10.4.44.101)_V_frames.txt"
filename = "../Data/Linphone_3g_20min_720p_4/Linphone_3g_20min_720p_4_1394698631_(213.87.130.168-to-10.4.44.101)_V_frames.txt"
Number, PacketCount, PacketCountMedian, PacketCountAverage, TimeStampSender, LastPacketReceptionTime, ReceptionDuration, IsComplete, MarkedPacketReceived, AreThereOutOfOrder, ReceiveSpeed, ReceiveSpeedMedian, State, IsInTime = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5,6,7,8,9,10,11,12,13), unpack=True, dtype=float)

#filename = "D:/?????/_??????/__? ??????/2015 - BB-LNCS/MSNetworkMonitor/CAPTools/x64/Debug/Linphone_3g_20min_720p_2/Estimate_2.txt"
filename = "../Data/Linphone_3g_20min_720p_4/Estimate_3.txt"
t, Prob1, Prob2, Prob3, E, F = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5), unpack=True, dtype=float)


#ReceiveSpeed = np.ndarray(shape=(len(Number),1), dtype = float); 
Ones = np.ndarray(shape=(len(Number),1), dtype = float); 
StateShifted = np.ndarray(shape=(len(Number),1), dtype = float); 
IsIncomplete = np.ndarray(shape=(len(Number),1), dtype = float); 
IsNotInTime = np.ndarray(shape=(len(Number),1), dtype = float); 

ReceiveSpeed_good = np.ndarray(shape=(1,1), dtype = float);
ReceiveSpeed_bad = np.ndarray(shape=(1,1), dtype = float);
ReceiveSpeed_ugly = np.ndarray(shape=(1,1), dtype = float);

for i in range(0, len(Number)-1):
    #ReceiveSpeed[i] = ReceptionDuration[i] / PacketCount[i]
    IsIncomplete[i] = 1 - IsComplete[i];
    IsNotInTime[i] = 1 - IsInTime[i];

    Ones[i] = 1;
    StateShifted[i] = 0.15 + 0.05 * State[i];
    if (State[i] == 0):
        if (len(ReceiveSpeed_good) == 1):
            ReceiveSpeed_good[0] = ReceiveSpeed[i]
        else:     
            ReceiveSpeed_good = np.append(ReceiveSpeed_good, ReceiveSpeed[i]);
    elif (State[i] == 1):
        if (len(ReceiveSpeed_bad) == 1):
            ReceiveSpeed_bad[0] = ReceiveSpeed[i];
        else:
            ReceiveSpeed_bad = np.append(ReceiveSpeed_bad, ReceiveSpeed[i])
    else:
        if (len(ReceiveSpeed_ugly) == 1):
            ReceiveSpeed_ugly[0] = ReceiveSpeed[i];
        else:
            ReceiveSpeed_ugly = np.append(ReceiveSpeed_ugly, ReceiveSpeed[i])


#            E1 = E
#E2 = E
#E3 = E

#for i in range(0, len(E) - 1):
#    if (E[i] == 0):
#        E1[i] = 1
#    else: E1[i] = 0;
#    if (E[i] == 1):
#        E2[i] = 1
#    else: E2[i] = 0;
#    if (E[i] == 2):
#        E2[i] = 1
#    else: E2[i] = 0;



#    print(ReceptionDuration[i])

fig, ax1 = plt.subplots()
ax1.plot(t, E+2, 'k-')
ax1.plot(t, Prob1, 'r-')
#ax1.set_xlim(1059,1076)
plt.show()

fig, ax1 = plt.subplots()
ax1.plot(t, E+2, 'k-')
ax1.plot(t, Prob2, 'g-')
#ax1.set_xlim(1059,1076)
plt.show()

fig, ax1 = plt.subplots()
ax1.plot(t, E+2, 'k-')
ax1.plot(t, Prob3, 'b-')
#ax1.set_xlim(1059,1076)
plt.show()



fig, ax1 = plt.subplots()
ax1.plot(LastPacketReceptionTime, ReceptionDuration, 'b-')
ax1.set_xlabel('LastPacketReceptionTime')
ax1.set_ylabel('Reception Duration', color='b')
ax1.set_ylim(0,1.5)
for tl in ax1.get_yticklabels():
    tl.set_color('b')


ax2 = ax1.twinx()
ax2.plot(LastPacketReceptionTime, PacketCount, 'c-')
ax2.plot(LastPacketReceptionTime, PacketCountMedian, 'k-')
ax2.plot(LastPacketReceptionTime, PacketCountAverage, 'r-')
ax2.plot(LastPacketReceptionTime, IsNotInTime, 'ko')
ax2.plot(LastPacketReceptionTime, IsIncomplete, 'yo')
ax2.set_ylabel('Packet count', color='b')
for tl in ax2.get_yticklabels():
    tl.set_color('r')
plt.show()


fig, ax1 = plt.subplots()
ax1.plot(LastPacketReceptionTime, ReceiveSpeed, 'b-')
ax1.plot(LastPacketReceptionTime, ReceiveSpeedMedian, 'y-')
ax1.plot(LastPacketReceptionTime, StateShifted, 'g-', linewidth=4)
ax1.plot(LastPacketReceptionTime, 0.015*Ones, 'k-')
ax1.plot(LastPacketReceptionTime, 0.04*Ones, 'k-')
#ax1.plot(LastPacketReceptionTime, 0.03*Ones, 'k-')
ax1.set_xlabel('LastPacketReceptionTime')
#ax1.set_xlim(75,85)
#ax1.set_xlim(0,1500)
ax1.set_ylim(0,0.25)
# Make the y-axis label and tick labels match the line color.
ax1.set_ylabel('ReceiveSpeed', color='b')
for tl in ax1.get_yticklabels():
    tl.set_color('b')


#fig, ax1 = plt.subplots()
#ax1.plot(LastPacketReceptionTime, PacketCount, 'b-')
#ax1.set_xlabel('LastPacketReceptionTime')
##ax1.set_xlim(0,30)
## Make the y-axis label and tick labels match the line color.
#ax1.set_ylabel('Packet count', color='b')
#for tl in ax1.get_yticklabels():
#    tl.set_color('b')


ax2 = ax1.twinx()
ax2.plot(LastPacketReceptionTime, PacketCount, 'r-')
ax2.plot(LastPacketReceptionTime, IsIncomplete, 'ko')
#ax2.set_ylabel('Reception duration', color='r')
ax2.set_ylabel('Packet count', color='b')
#ax2.set_xlim(0,1500)
#ax2.set_xlim(75,85)
#ax2.set_ylim(0,0.5)
for tl in ax2.get_yticklabels():
    tl.set_color('r')
plt.show()


#pylab.figure();
#n, bins, patches = pylab.hist(ReceiveSpeed_good, 500, normed=1, histtype='stepfilled')
#pylab.setp(patches, 'facecolor', 'g', 'alpha', 0.75)
##pylab.ylim(0,5)
##pylab.xlim(0,0.5)
##l = pylab.plot(bins, 'k--', linewidth=1.5)
#pylab.show()

#pylab.figure();
#n, bins, patches = pylab.hist(ReceiveSpeed_bad, 500, normed=1, histtype='stepfilled')
#pylab.setp(patches, 'facecolor', 'g', 'alpha', 0.75)
##pylab.ylim(0,5)
##pylab.xlim(0,0.5)
##l = pylab.plot(bins, 'k--', linewidth=1.5)
#pylab.show()

#pylab.figure();
#n, bins, patches = pylab.hist(ReceiveSpeed_ugly, 500, normed=1, histtype='stepfilled')
#pylab.setp(patches, 'facecolor', 'g', 'alpha', 0.75)
##pylab.ylim(0,5)
##pylab.xlim(0,0.5)
##l = pylab.plot(bins, 'k--', linewidth=1.5)
#pylab.show()



#onesOffsetHost1 = np.ones(OffsetHost1.shape)
#onesOffsetHost2 = np.ones(OffsetHost2.shape)
#pylab.figure();
#n, bins, patches = pylab.hist(OffsetHostDiff0, 500, normed=1, histtype='stepfilled')
#pylab.setp(patches, 'facecolor', 'g', 'alpha', 0.75)
#pylab.ylim(0,5)
#pylab.xlim(0,0.5)
##l = pylab.plot(bins, 'k--', linewidth=1.5)
#pylab.show()


#fig = plt.figure(num=None, figsize=(12, 4), dpi=150, facecolor='w', edgecolor='k')
#ax = plt.subplot(111)

#ax.plot(OffsetHost0,  OffsetHostDiff0, '-', color='grey', linewidth=1)
#ax.plot(OffsetHost1, 0.16 * onesOffsetHost1, '.', color = 'black' )
#ax.plot(OffsetHost2, 0.16 * onesOffsetHost2, 'x', color = 'black' )
#ax.set_xlim(0,60.91)
#plt.show()

#fig = plt.figure(num=None, figsize=(12, 4), dpi=150, facecolor='w', edgecolor='k')
#ax = plt.subplot(111)

#ax.plot(OffsetSource0,  OffsetSourceDiff0, '-', color='grey', linewidth=1)
#ax.plot(OffsetHost1, 0.16 * onesOffsetHost1, '.', color = 'black' )
#ax.plot(OffsetHost2, 0.16 * onesOffsetHost2, 'x', color = 'black' )
#ax.set_xlim(0,60.91)
#plt.show()

#fig = plt.figure(num=None, figsize=(12, 4), dpi=150, facecolor='w', edgecolor='k')
#ax = plt.subplot(111)

#ax.plot(OffsetHost0,  OffsetHostSourceDiff0, '-', color='grey', linewidth=1)
#ax.plot(OffsetHost1, 0.16 * onesOffsetHost1, '.', color = 'black' )
#ax.plot(OffsetHost2, 0.16 * onesOffsetHost2, 'x', color = 'black' )
#ax.set_xlim(0,60.91)
#plt.show()
