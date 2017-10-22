import matplotlib
import matplotlib.pyplot as plt
import numpy as np
import scipy.io as io
import matplotlib.tri as mtri
matplotlib.rc('text', usetex = True)
import pylab
#import OutputParcerFunction
import scipy.stats as stats

#==== 0 0 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_timeinstate_data.txt"
gamma_data0 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_timeinstate_params.txt"
a0 = np.loadtxt(filename, delimiter = ' ', unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_timeinstate_data.txt"
gamma_data1 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_timeinstate_params.txt"
a1 = np.loadtxt(filename, delimiter = ' ', unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_timeinstate_data.txt"
gamma_data2 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_timeinstate_params.txt"
a2 = np.loadtxt(filename, delimiter = ' ', unpack=True, dtype=float)



from pylab import *

f = plt.figure(num=None, figsize=(10, 2.8), dpi=150, facecolor='w', edgecolor='k')
#subplots_adjust(hspace=0.0)

plt.subplots_adjust(left=0.04, bottom=0.075, right=0.98, top=0.91)

maxx = 100
_bins = np.linspace(0,maxx,12)
x = np.linspace(1, maxx, 1000)
y0 = stats.expon.pdf(x,0,a0)
y1 = stats.expon.pdf(x,0,a1)
y2 = stats.expon.pdf(x,0,a2)

for i in range(1, len(x)-1):
    y0[i] = 1/a0*exp(-1/a0*x[i])
    y1[i] = 1/a1*exp(-1/a1*x[i])
    y2[i] = 1/a2*exp(-1/a2*x[i])


#print (x)
#print (y0)

#maxx = 65;
maxy = 0.13
subplots_adjust(wspace=0.12)
ax0 = plt.subplot(131)
plt.title('$i=1$')
ax0.hist(gamma_data0, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75,  linewidth = 2.0)
ax0.plot(x,y0, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax0.set_xlim(0,maxx)
ax0.set_ylim(0,maxy)
ax0.set_xticks([0,50,100]);
#ax0.set_xticks([0,50]);
ax0.set_yticks([0,0.1]);



ax1 = plt.subplot(132)
plt.title('$i=2$')
ax1.hist(gamma_data1, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75,linewidth = 2.0)
ax1.plot(x,y1, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax1.set_xlim(0,maxx)
ax1.set_ylim(0,maxy)
ax1.set_xticks([0,50,100]);
#ax1.set_xticks([0,50]);
ax1.set_yticks([0,0.1]);

ax2 = plt.subplot(133)
plt.title('$i=3$')
ax2.hist(gamma_data2, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linewidth = 2.0)
ax2.plot(x,y2, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax2.set_xlim(0,maxx)
ax2.set_ylim(0,maxy)
ax2.set_xticks([0,50,100]);
#ax2.set_xticks([0,50]);
ax2.set_yticks([0,0.1]);
#ax0_0.hist(gamma_data001, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax0_0.plot(x,y001, '-', color="grey", alpha=0.85)

#ax0_0.hist(gamma_data002, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax0_0.plot(x,y002, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax0_0.set_xlim(0,maxx)

#ax0_1 = plt.subplot(222)
#plt.title('$Y_n = f_0$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
#ax0_1.hist(gamma_data010, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax0_1.plot(x,y010, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax0_1.hist(gamma_data011, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax0_1.plot(x,y011, '-', color="grey", alpha=0.85)

#ax0_1.hist(gamma_data012, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax0_1.plot(x,y012, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax0_1.set_xlim(0,maxx)


#ax1_0 = plt.subplot(223)
#plt.title('$Y_n = f_1$, $Y_{n-1} = f_0$, $X_{n-1} = e_i$')
#ax1_0.hist(gamma_data100, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax1_0.plot(x,y100, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax1_0.hist(gamma_data101, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax1_0.plot(x,y101, '-', color="grey", alpha=0.85)

#ax1_0.hist(gamma_data102, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax1_0.plot(x,y102, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax1_0.set_xlim(0,maxx)


#ax1_1 = plt.subplot(224)
#plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
#ax1_1.hist(gamma_data110, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax1_1.plot(x,y110, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax1_1.hist(gamma_data111, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax1_1.plot(x,y111, '-', color="grey", alpha=0.85)

#ax1_1.hist(gamma_data112, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax1_1.plot(x,y112, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax1_1.set_xlim(0,maxx)

savefilename = u"D:/Наука/_Статьи/__в работе/2015 - new2an - rus/pic_timesinstates_hists.pdf";
f.savefig(savefilename)
#show()


#ax1_1 = plt.subplot(224)
#plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_1$')
#ax1_1.hist(gamma_data110, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dotted')
#ax1_1.plot(x,y110, '-', color="black", linestyle = 'dotted')

#ax1_1.hist(gamma_data111, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax1_1.plot(x,y111, '-', color="grey", alpha=0.85)

#ax1_1.hist(gamma_data112, _bins_big, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.50, linewidth = 3.0)
#ax1_1.plot(x,y112, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax1_1.set_xlim(0,maxx)

