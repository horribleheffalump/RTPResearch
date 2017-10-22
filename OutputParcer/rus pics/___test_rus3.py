# -*- coding: utf-8 -*-
from __future__ import (absolute_import, division, print_function,
                        unicode_literals)

#from matplotlib.externals import six

import matplotlib as mpl
mpl.use("pgf")
pgf_with_custom_preamble = {
    "font.family": "serif", # use serif/main font for text elements
    "text.usetex": True,    # use inline math for ticks
    "pgf.rcfonts": False,   # don't setup fonts from rc parameters
    "pgf.preamble": [
         "\\usepackage{units}",         # load additional packages
         "\\usepackage{metalogo}",
         "\\usepackage{unicode-math}",  # unicode math setup
         r"\setmathfont{xits-math.otf}",
         r"\setmainfont{DejaVu Serif}", # serif font via preamble
         ]
}
mpl.rcParams.update(pgf_with_custom_preamble)

import matplotlib.pyplot as plt
import numpy as np
from pylab import *

rcv_spd_lb = 0.04
rcv_spd_ub = 0.08


#filename = u"../Data/data 2015-12-9 10-57 - 3g 20min - estimate by the  first quater - 0.04 lb/frames.txt"
filename = u"../Data/data 2015-12-10 18-36 - 3g 20min - online estimate - 0.04 lb/frames.txt"
Number, PacketCount, PacketCountMedian, PacketCountAverage, TimeStampSender, LastPacketReceptionTime, ReceptionDuration, IsComplete, MarkedPacketReceived, AreThereOutOfOrder, ReceiveSpeed, ReceiveSpeedMedian, IsInTime = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5,6,7,8,9,10,11,12), unpack=True, dtype=float)

#filename = u"../Data/data 2015-12-9 10-57 - 3g 20min - estimate by the  first quater - 0.04 lb/Estimate_0.txt"
filename = u"../Data/data 2015-12-10 18-36 - 3g 20min - online estimate - 0.04 lb/Estimate_0.txt"
t, Prob1, Prob2, Prob3, E, F = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2,3,4,5), unpack=True, dtype=float)


IsIncomplete = []
TIsIncomplete = []
IsNotInTime = []
TIsNotInTime = []


for i in range(0, len(Number)-1):
    if (IsComplete[i] == 0):
        IsIncomplete.append(-0.02)
        TIsIncomplete.append(LastPacketReceptionTime[i]) 
    if (IsInTime[i] == 0):
        IsNotInTime.append(-0.01)
        TIsNotInTime.append(LastPacketReceptionTime[i]) 




plt.figure(num=None, figsize=(10, 4.5), dpi=150, facecolor='w', edgecolor='k')
subplots_adjust(hspace=0.0)

plt.subplots_adjust(left=0.06, bottom=0.07, right=0.95, top=0.95, wspace=0.1)

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
ax1.plot(TIsIncomplete, IsIncomplete, 'k.')
#ax1.fill_between(t, leveltwo, levelthree, where=E==o, color='black', alpha = 0.2, linewidth=0.0);
#ax1.fill_between(t, levelone, leveltwo, where=E==ones, color='black', alpha = 0.4, linewidth=0.0);
#ax1.fill_between(t, levelzero, levelone, where=E==ones*2, color='black', alpha = 0.8, linewidth=0.0);
ax1.plot(t, rcv_spd_lb*ones, 'k--', linewidth=0.75)
ax1.plot(t, rcv_spd_ub*ones, 'k--', linewidth=0.75)
ax1.set_yticks([0,0.1,0.2,0.3])
ax1.set_ylim(-0.07,0.3)



plt.savefig(u"d:/fig.pdf")
#plt.tight_layout(.5)