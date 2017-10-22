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
#filename = u"../Data/data 2015-12-10 18-36 - 3g 20min - online estimate - 0.04 lb/Estimate_0.txt"
filename = u"../Data/data 2015-12-11 18-42 - 3g 20min - online estimate - 0.04 lb - exp/Estimate_0.txt"
t, Prob1, Prob2, Prob3, E, F = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5), unpack=True, dtype=float)

tshort = [];
P1 = [];
P2 = [];
P3 = [];

for i in range(0, len(t)-1):
    if (Prob1[i] + Prob2[i] + Prob3[i] > 0):
        tshort.append(t[i])
        P1.append(Prob1[i])
        P2.append(Prob2[i])
        P3.append(Prob3[i])


#E1 = np.ndarray(shape=(len(E),1), dtype = float); 
#E2 = np.ndarray(shape=(len(E),1), dtype = float); 
#E3 = np.ndarray(shape=(len(E),1), dtype = float); 
#E1 = np.zeros((len(E), 1), np.float); #np.array(len(E)); 
#E2 = np.zeros((len(E), 1), np.float);
#E3 = np.zeros((len(E), 1), np.float);
#wh = np.zeros((len(E), 1), np.bool);
#lbarr = np.zeros((len(E), 1), np.float);
#tt = np.zeros((len(E), 1), np.float); 

#t = array(t)

ub = 1.15
lb = -0.15

#for i in range(1, len(E)-2):
#    #tt[i] = t[i];
#    #lbarr[i] = lb;
#    if E[i] == 0: 
#        if (1<i): 
#            E1[i]=ub;
#            #wh[i] = 1;
#    else:
#        E1[1]=lb;
#        #wh[i] = 0;
#    if E[i] == 1: 
#        E2[i]=ub;
#    else:
#        E2[1]=lb;
#    if E[i] == 2: 
#        E3[i]=ub;
#    else:
#        E3[1]=lb;
            

#print(len(t));
#print(len(tt));
#print(len(E1));
#print(len(lbarr));

from pylab import *


#f = plt.figure(num=None, figsize=(10, 5), dpi=150, facecolor='w', edgecolor='k')
#ax = subplot(311)
#ax.plot(t, Prob1, marker='.', lw=1)
#d = np.zeros(len(Prob1))
#o = np.ones(len(Prob1))
#ax.fill_between(t, d, E, where=E==o, interpolate=True, color='blue')

f = plt.figure(num=None, figsize=(10, 4.5), dpi=150, facecolor='w', edgecolor='k')
subplots_adjust(hspace=0.0)

plt.subplots_adjust(left=0.06, bottom=0.07, right=0.98, top=0.95, wspace=0.1)



o = np.zeros(len(Prob1))
ones = np.ones(len(Prob1))
up = np.ones(len(Prob1))*ub
low = np.ones(len(Prob1))*lb
ax1 = subplot(311)
ax1.plot(tshort, P1,color='black')
ax1.fill_between(t, low, up, where=E==o, color='black', alpha = 0.2, linewidth=0.0);
ax1.set_yticks([0,1])
ax1.set_ylim(lb,ub)
ax1.text(-120, 0.5, '$i=1$')
#ax1.axvline(x=300, linewidth=1.2, color='black');

ax2 = subplot(312, sharex=ax1)
ax2.plot(tshort,P2,color='black')
ax2.fill_between(t, low, up, where=E==ones, color='black', alpha = 0.4, linewidth=0.0);
ax2.set_yticks([0,1])
ax2.set_ylim(lb,ub)
ax2.text(-120, 0.5, '$i=2$')
#ax2.axvline(x=300, linewidth=1.2, color='black');

ax3 = subplot(313, sharex=ax1)
ax3.plot(tshort,P3,color='black')
ax3.fill_between(t, low, up, where=E==ones*2, color='black', alpha = 0.7, linewidth=0.0);
ax3.set_yticks([0,1])
ax3.set_ylim(lb,ub)
ax3.text(-120, 0.5, '$i=3$')
#ax3.axvline(x=300, linewidth=1.2, color='black');

xticklabels = ax1.get_xticklabels()+ax2.get_xticklabels()
setp(xticklabels, visible=False)

#ax1.spines['right'].set_visible(False)
#ax1.spines['top'].set_visible(False)
ax1.spines['bottom'].set_visible(False)
ax1.yaxis.set_ticks_position('both')
ax1.xaxis.set_ticks_position('none')
#ax2.spines['right'].set_visible(False)
ax2.spines['top'].set_visible(False)
ax2.spines['bottom'].set_visible(False)
ax2.yaxis.set_ticks_position('both')
ax2.xaxis.set_ticks_position('none')
#ax3.spines['right'].set_visible(False)
ax3.spines['top'].set_visible(False)
ax3.yaxis.set_ticks_position('both')
ax3.xaxis.set_ticks_position('bottom')

ax3.set_xlim(-50,t[len(E)-1] + 50);
#ax3.set_xticks([0,500,1000,1500]);
#ax3.set_xticks([0,500,1000]);

xticks = [0, 300, 600, 900, 1200]
#xticklabels = ['0', '{\bf 300}', '600', '900', '1200']
ax3.set_xticks(xticks);

f.canvas.draw()
labels = [item.get_text() for item in ax3.get_xticklabels()]
labels[0] = u'$\mathrm{0min}$'
labels[1] = u'$\mathrm{5min}$'
labels[2] = u'$\mathrm{10min}$'
labels[3] = u'$\mathrm{15min}$'
labels[4] = u'$\mathrm{20min}$'

ax3.set_xticklabels(labels)



savefilename = u"D:/Наука/_Статьи/__в работе/2015 - new2an - rus/pic_estimates_exp.pdf";
f.savefig(savefilename)
#show()