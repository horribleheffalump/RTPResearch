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
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_0/0_0_0_gamma_data.txt"
gamma_data000 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_0/0_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a000,b000 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_0/0_1_0_gamma_data.txt"
gamma_data010 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_0/0_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a010,b010 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_0/0_2_0_gamma_data.txt"
gamma_data020 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_0/0_2_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a020,b020 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#==== 0 1 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_1/0_0_1_gamma_data.txt"
gamma_data001 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_1/0_0_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a001,b001 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_1/0_1_1_gamma_data.txt"
gamma_data011 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_1/0_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a011,b011 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_1/0_2_1_gamma_data.txt"
gamma_data021 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_1/0_2_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a021,b021 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#==== 0 2 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_2/0_0_2_gamma_data.txt"
gamma_data002 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_2/0_0_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a002,b002 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_2/0_1_2_gamma_data.txt"
gamma_data012 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_2/0_1_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a012,b012 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_2/0_2_2_gamma_data.txt"
gamma_data022 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/0_2/0_2_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a022,b022 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#==== 1 0 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_0/1_0_0_gamma_data.txt"
gamma_data100 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_0/1_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a100,b100 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_0/1_1_0_gamma_data.txt"
gamma_data110 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_0/1_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a110,b110 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_0/1_2_0_gamma_data.txt"
gamma_data120 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_0/1_2_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a120,b120 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#==== 1 1 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_1/1_0_1_gamma_data.txt"
gamma_data101 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_1/1_0_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a101,b101 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_1/1_1_1_gamma_data.txt"
gamma_data111 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_1/1_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a111,b111 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_1/1_2_1_gamma_data.txt"
gamma_data121 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_1/1_2_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a121,b121 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#==== 1 2 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_2/1_0_2_gamma_data.txt"
gamma_data102 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_2/1_0_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a102,b102 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_2/1_1_2_gamma_data.txt"
gamma_data112 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_2/1_1_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a112,b112 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_2/1_2_2_gamma_data.txt"
gamma_data122 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/1_2/1_2_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a122,b122 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)


#==== 2 0 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_0/2_0_0_gamma_data.txt"
gamma_data200 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_0/2_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a200,b200 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_0/2_1_0_gamma_data.txt"
gamma_data210 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_0/2_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a210,b210 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_0/2_2_0_gamma_data.txt"
gamma_data220 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_0/2_2_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a220,b220 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#==== 2 1 ======

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_1/2_0_1_gamma_data.txt"
gamma_data201 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_1/2_0_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a201,b201 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_1/2_1_1_gamma_data.txt"
gamma_data211 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_1/2_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a211,b211 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_1/2_2_1_gamma_data.txt"
gamma_data221 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_1/2_2_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a221,b221 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#==== 2 2 ======

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_2/2_0_2_gamma_data.txt"
gamma_data202 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_2/2_0_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a202,b202 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_2/2_1_2_gamma_data.txt"
gamma_data212 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_2/2_1_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a212,b212 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_2/2_2_2_gamma_data.txt"
gamma_data222 = np.loadtxt(filename, unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/gamma_on_all_observations/2_2/2_2_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
a222,b222 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)


from pylab import *

f = plt.figure(num=None, figsize=(10, 5.5), dpi=150, facecolor='w', edgecolor='k')
#subplots_adjust(hspace=0.0)

plt.subplots_adjust(left=0.04, bottom=0.04, right=0.98, top=0.95)

maxx = 0.5
maxx_1 = 1.0
maxx_big = 5
_bins = np.linspace(0,maxx+0.3,20)
_bins[19] = 100
_bins_big = np.linspace(0,maxx_big,25)
maxy = 30.0
x = np.linspace(0, maxx*10, 1000)
y000 = stats.gamma.pdf(x,a000,0,b000)
y010 = stats.gamma.pdf(x,a010,0,b010)
y020 = stats.gamma.pdf(x,a020,0,b020)

y001 = stats.gamma.pdf(x,a001,0,b001)
y011 = stats.gamma.pdf(x,a011,0,b011)
y021 = stats.gamma.pdf(x,a021,0,b021)

y002 = stats.gamma.pdf(x,a002,0,b002)
y012 = stats.gamma.pdf(x,a012,0,b012)
y022 = stats.gamma.pdf(x,a022,0,b022)

y100 = stats.gamma.pdf(x,a100,0,b100)
y110 = stats.gamma.pdf(x,a110,0,b110)
y120 = stats.gamma.pdf(x,a120,0,b120)

y101 = stats.gamma.pdf(x,a101,0,b101)
y111 = stats.gamma.pdf(x,a111,0,b111)
y121 = stats.gamma.pdf(x,a121,0,b121)

y102 = stats.gamma.pdf(x,a102,0,b102)
y112 = stats.gamma.pdf(x,a112,0,b112)
y122 = stats.gamma.pdf(x,a122,0,b122)

y200 = stats.gamma.pdf(x,a200,0,b200)
y210 = stats.gamma.pdf(x,a210,0,b210)
y220 = stats.gamma.pdf(x,a220,0,b220)

y201 = stats.gamma.pdf(x,a201,0,b201)
y211 = stats.gamma.pdf(x,a211,0,b211)
y221 = stats.gamma.pdf(x,a221,0,b221)

y202 = stats.gamma.pdf(x,a202,0,b202)
y212 = stats.gamma.pdf(x,a212,0,b212)
y222 = stats.gamma.pdf(x,a222,0,b222)


subplots_adjust(hspace=0.15)
subplots_adjust(wspace=0.1)
ax0_0 = plt.subplot(221)
plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
ax0_0.hist(gamma_data000, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
ax0_0.plot(x,y000, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

ax0_0.hist(gamma_data001, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
ax0_0.plot(x,y001, '-', color="grey", alpha=0.85)

ax0_0.hist(gamma_data002, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
ax0_0.plot(x,y002, '-', color="black", alpha=0.50, linewidth = 3.0)

ax0_0.set_xlim(0,maxx)
ax0_0.set_xticks([0,0.8]);
ax0_0.set_yticks([0,9]);

ax0_1 = plt.subplot(222)
plt.title('$Y_n = f_1$, $Y_{n-1} = f_2$, $X_{n-1} = e_i$')
ax0_1.hist(gamma_data010, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
ax0_1.plot(x,y010, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

ax0_1.hist(gamma_data011, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
ax0_1.plot(x,y011, '-', color="grey", alpha=0.85)

ax0_1.hist(gamma_data012, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
ax0_1.plot(x,y012, '-', color="black", alpha=0.50, linewidth = 3.0)

ax0_1.set_xlim(0,maxx)
ax0_1.set_xticks([0,0.8]);
ax0_1.set_yticks([0,9]);

#ax0_2 = plt.subplot(333)
#plt.title('$Y_n = f_1$, $Y_{n-1} = f_3$, $X_{n-1} = e_i$')
#ax0_2.hist(gamma_data020, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax0_2.plot(x,y020, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax0_2.hist(gamma_data021, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax0_2.plot(x,y021, '-', color="grey", alpha=0.85)

#ax0_2.hist(gamma_data022, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax0_2.plot(x,y022, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax0_2.set_xlim(0,maxx)
#ax0_2.set_xticks([0,0.8]);
#ax0_2.set_yticks([0,9]);


ax1_0 = plt.subplot(223)
plt.title('$Y_n = f_2$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
ax1_0.hist(gamma_data100, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
ax1_0.plot(x,y100, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

ax1_0.hist(gamma_data101, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
ax1_0.plot(x,y101, '-', color="grey", alpha=0.85)

ax1_0.hist(gamma_data102, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
ax1_0.plot(x,y102, '-', color="black", alpha=0.50, linewidth = 3.0)

ax1_0.set_xlim(0,maxx)
ax1_0.set_xticks([0,0.8]);
ax1_0.set_yticks([0,16]);


ax1_1 = plt.subplot(224)
plt.title('$Y_n = f_2$, $Y_{n-1} = f_2$, $X_{n-1} = e_i$')
ax1_1.hist(gamma_data110, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
ax1_1.plot(x,y110, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

ax1_1.hist(gamma_data111, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
ax1_1.plot(x,y111, '-', color="grey", alpha=0.85)

ax1_1.hist(gamma_data112, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
ax1_1.plot(x,y112, '-', color="black", alpha=0.50, linewidth = 3.0)

ax1_1.set_xlim(0,maxx)
ax1_1.set_xticks([0,0.8]);
ax1_1.set_yticks([0,16]);


#ax1_2 = plt.subplot(336)
#plt.title('$Y_n = f_2$, $Y_{n-1} = f_3$, $X_{n-1} = e_i$')
#ax1_2.hist(gamma_data120, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax1_2.plot(x,y120, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax1_2.hist(gamma_data121, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax1_2.plot(x,y121, '-', color="grey", alpha=0.85)

#ax1_2.hist(gamma_data122, _bins, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax1_2.plot(x,y122, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax1_2.set_xlim(0,maxx)
#ax1_2.set_xticks([0,0.8]);
#ax1_2.set_yticks([0,16]);



#ax2_0 = plt.subplot(337)
#plt.title('$Y_n = f_3$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
#ax2_0.hist(gamma_data200, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax2_0.plot(x,y200, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax2_0.hist(gamma_data201, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax2_0.plot(x,y201, '-', color="grey", alpha=0.85)

#ax2_0.hist(gamma_data202, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax2_0.plot(x,y202, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax2_0.set_xlim(0,maxx)
#ax2_0.set_xticks([0,0.8]);
#ax2_0.set_yticks([0,16]);


#ax2_1 = plt.subplot(338)
#plt.title('$Y_n = f_3$, $Y_{n-1} = f_2$, $X_{n-1} = e_i$')
#ax2_1.hist(gamma_data210, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax2_1.plot(x,y210, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax2_1.hist(gamma_data211, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax2_1.plot(x,y211, '-', color="grey", alpha=0.85)

#ax2_1.hist(gamma_data212, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax2_1.plot(x,y212, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax2_1.set_xlim(0,maxx)
#ax2_1.set_xticks([0,0.8]);
#ax2_1.set_yticks([0,16]);


#ax2_2 = plt.subplot(339)
#plt.title('$Y_n = f_3$, $Y_{n-1} = f_3$, $X_{n-1} = e_i$')
#ax2_2.hist(gamma_data220, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax2_2.plot(x,y220, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax2_2.hist(gamma_data221, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax2_2.plot(x,y221, '-', color="grey", alpha=0.85)

#ax2_2.hist(gamma_data222, _bins, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax2_2.plot(x,y222, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax2_2.set_xlim(0,maxx)
#ax2_2.set_xticks([0,0.8]);
#ax2_2.set_yticks([0,16]);

savefilename = u"D:/Наука/_Статьи/__в работе/2015 - new2an - rus/pic_hists.pdf";
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





## картинка для первой статьи

##==== 0 0 ======
#filename = u"../Data/Linphone_3g_20min_720p_2/0_0/0_0_0_gamma_data.txt"
#gamma_data000 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_0/0_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a000,b000 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_0/0_1_0_gamma_data.txt"
#gamma_data010 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_0/0_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a010,b010 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_0/0_2_0_gamma_data.txt"
#gamma_data020 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_0/0_2_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a020,b020 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

##==== 0 1 ======
#filename = u"../Data/Linphone_3g_20min_720p_2/0_1/0_0_1_gamma_data.txt"
#gamma_data001 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_1/0_0_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a001,b001 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_1/0_1_1_gamma_data.txt"
#gamma_data011 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_1/0_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a011,b011 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_1/0_2_1_gamma_data.txt"
#gamma_data021 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_1/0_2_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a021,b021 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

##==== 0 2 ======
#filename = u"../Data/Linphone_3g_20min_720p_2/0_2/0_0_2_gamma_data.txt"
#gamma_data002 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_2/0_0_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a002,b002 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_2/0_1_2_gamma_data.txt"
#gamma_data012 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/0_2/0_1_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a012,b012 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

##==== 1 0 ======
#filename = u"../Data/Linphone_3g_20min_720p_2/1_0/1_0_0_gamma_data.txt"
#gamma_data100 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_0/1_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a100,b100 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_0/1_1_0_gamma_data.txt"
#gamma_data110 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_0/1_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a110,b110 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_0/1_2_0_gamma_data.txt"
#gamma_data120 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_0/1_2_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a120,b120 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

##==== 1 1 ======
#filename = u"../Data/Linphone_3g_20min_720p_2/1_1/1_0_1_gamma_data.txt"
#gamma_data101 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_1/1_0_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a101,b101 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_1/1_1_1_gamma_data.txt"
#gamma_data111 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_1/1_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a111,b111 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_1/1_2_1_gamma_data.txt"
#gamma_data121 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_1/1_2_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a121,b121 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

##==== 1 2 ======
#filename = u"../Data/Linphone_3g_20min_720p_2/1_2/1_0_2_gamma_data.txt"
#gamma_data102 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_2/1_0_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a102,b102 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_2/1_1_2_gamma_data.txt"
#gamma_data112 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/1_2/1_1_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a112,b112 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

##==== 2 0 ======
#filename = u"../Data/Linphone_3g_20min_720p_2/2_0/2_0_0_gamma_data.txt"
#gamma_data200 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/2_0/2_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a200,b200 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/2_0/2_1_0_gamma_data.txt"
#gamma_data210 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/2_0/2_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a210,b210 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)

##==== 2 1 ======

#filename = u"../Data/Linphone_3g_20min_720p_2/2_1/2_1_1_gamma_data.txt"
#gamma_data211 = np.loadtxt(filename, unpack=True, dtype=float)

#filename = u"../Data/Linphone_3g_20min_720p_2/2_1/2_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
#a211,b211 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1), unpack=True, dtype=float)


#from pylab import *

#f = plt.figure(num=None, figsize=(10, 5), dpi=150, facecolor='w', edgecolor='k')
##subplots_adjust(hspace=0.0)

#plt.subplots_adjust(left=0.04, bottom=0.04, right=0.98, top=0.95)

#maxx = 0.5
#maxx_1 = 1.0
#maxx_big = 5
#_bins = np.linspace(0,maxx+0.2,27)
#_bins[26] = 100
#_bins_big = np.linspace(0,maxx_big,25)
#maxy = 30.0
#x = np.linspace(0, maxx*10, 1000)
#y000 = stats.gamma.pdf(x,a000,0,b000)
#y010 = stats.gamma.pdf(x,a010,0,b010)
#y020 = stats.gamma.pdf(x,a020,0,b020)

#y001 = stats.gamma.pdf(x,a001,0,b001)
#y011 = stats.gamma.pdf(x,a011,0,b011)
#y021 = stats.gamma.pdf(x,a021,0,b021)

#y002 = stats.gamma.pdf(x,a002,0,b002)
#y012 = stats.gamma.pdf(x,a012,0,b012)

#y100 = stats.gamma.pdf(x,a100,0,b100)
#y110 = stats.gamma.pdf(x,a110,0,b110)
#y120 = stats.gamma.pdf(x,a120,0,b120)

#y101 = stats.gamma.pdf(x,a101,0,b101)
#y111 = stats.gamma.pdf(x,a111,0,b111)
#y121 = stats.gamma.pdf(x,a121,0,b121)

#y102 = stats.gamma.pdf(x,a102,0,b102)
#y112 = stats.gamma.pdf(x,a112,0,b112)

#y200 = stats.gamma.pdf(x,a200,0,b200)
#y210 = stats.gamma.pdf(x,a210,0,b210)

#y211 = stats.gamma.pdf(x,a211,0,b211)

##print(y)
##ax0_0 = plt.subplot(331)
##ax0_0.hist(gamma_data000, 30, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax0_0.plot(x,y000, '-', color="black", )
##ax0_0.hist(gamma_data010, 20, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax0_0.plot(x,y010, '-', color="black", )
##ax0_0.hist(gamma_data020, 3, normed=1, histtype='stepfilled', facecolor='white', alpha=0.75)
##ax0_0.plot(x,y020, '-', color="black", )
##ax0_0.set_xlim(0,maxx)
##ax0_0.set_ylim(0,maxy)

##ax0_1 = plt.subplot(332)
##ax0_1.hist(gamma_data001, 30, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax0_1.plot(x,y001, '-', color="black", )
##ax0_1.hist(gamma_data011, 20, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax0_1.plot(x,y011, '-', color="black", )
##ax0_1.hist(gamma_data021, 3, normed=1, histtype='stepfilled', facecolor='white', alpha=0.75)
##ax0_1.plot(x,y021, '-', color="black", )
##ax0_1.set_xlim(0,maxx)
##ax0_1.set_ylim(0,maxy)

##ax0_1 = plt.subplot(333)
##ax0_1.hist(gamma_data002, 10, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax0_1.plot(x,y002, '-', color="black", )
##ax0_1.hist(gamma_data012, 10, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax0_1.plot(x,y012, '-', color="black", )
##ax0_1.set_xlim(0,maxx*10)
##ax0_1.set_ylim(0,maxy/10)

##ax1_0 = plt.subplot(334)
##ax1_0.hist(gamma_data100, 30, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax1_0.plot(x,y100, '-', color="black", )
##ax1_0.hist(gamma_data110, 20, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax1_0.plot(x,y110, '-', color="black", )
##ax1_0.hist(gamma_data120, 3, normed=1, histtype='stepfilled', facecolor='white', alpha=0.75)
##ax1_0.plot(x,y120, '-', color="black", )
##ax1_0.set_xlim(0,maxx)
##ax1_0.set_ylim(0,maxy)

##ax1_1 = plt.subplot(335)
##ax1_1.hist(gamma_data101, 30, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax1_1.plot(x,y101, '-', color="black", )
##ax1_1.hist(gamma_data111, 20, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax1_1.plot(x,y111, '-', color="black", )
##ax1_1.hist(gamma_data121, 3, normed=1, histtype='stepfilled', facecolor='white', alpha=0.75)
##ax1_1.plot(x,y121, '-', color="black", )
##ax1_1.set_xlim(0,maxx)
##ax1_1.set_ylim(0,maxy)

##ax1_1 = plt.subplot(336)
##ax1_1.hist(gamma_data102, 10, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax1_1.plot(x,y102, '-', color="black", )
##ax1_1.hist(gamma_data112, 10, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax1_1.plot(x,y112, '-', color="black", )
##ax1_1.set_xlim(0,maxx*10)
##ax1_1.set_ylim(0,maxy/10)

##ax2_0 = plt.subplot(337)
##ax2_0.hist(gamma_data200, 5, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax2_0.plot(x,y200, '-', color="black", )
##ax2_0.hist(gamma_data210, 5, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax2_0.plot(x,y210, '-', color="black", )
##ax2_0.set_xlim(0,maxx)
##ax2_0.set_ylim(0,maxy)

##ax2_1 = plt.subplot(338)
##ax2_1.hist(gamma_data211, 5, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax2_1.plot(x,y211, '-', color="black", )
##ax2_1.set_xlim(0,maxx)
##ax2_1.set_ylim(0,maxy)
##show()
##subplots_adjust(hspace=0.4)
##ax0_0_0 = plt.subplot(321)
##plt.title('$Y_n = f_0$, $Y_{n-1} = f_0$, $X_{n-1} = e_0$')
##ax0_0_0.hist(gamma_data000, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax0_0_0.plot(x,y000, '-', color="black", )
##ax0_1_0 = plt.subplot(322)
##plt.title('$Y_n = f_0$, $Y_{n-1} = f_1$, $X_{n-1} = e_0$')
##ax0_1_0.hist(gamma_data010, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax0_1_0.plot(x,y010, '-', color="black", )
###ax0_2_0 = plt.subplot(333)
###plt.title('$Y_n = f_0$, $Y_{n-1} = f_2$, $X_{n-1} = e_0$')
###ax0_2_0.hist(gamma_data020, 100, normed=1, histtype='stepfilled', facecolor='white', alpha=0.75)
###ax0_2_0.plot(x,y020, '-', color="black", )
##ax0_0_0.set_xlim(0,maxx)
##ax0_1_0.set_xlim(0,maxx)
###ax0_2_0.set_xlim(0,maxx)

##ax0_0_1 = plt.subplot(323)
##plt.title('$Y_n = f_0$, $Y_{n-1} = f_0$, $X_{n-1} = e_1$')
##ax0_0_1.hist(gamma_data001, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax0_0_1.plot(x,y001, '-', color="black", )
##ax0_1_1 = plt.subplot(324)
##plt.title('$Y_n = f_0$, $Y_{n-1} = f_1$, $X_{n-1} = e_1$')
##ax0_1_1.hist(gamma_data011, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax0_1_1.plot(x,y011, '-', color="black", )
###ax0_2_1 = plt.subplot(336)
###plt.title('$Y_n = f_0$, $Y_{n-1} = f_2$, $X_{n-1} = e_1$')
###ax0_2_1.hist(gamma_data021, 100, normed=1, histtype='stepfilled', facecolor='white', alpha=0.75)
###ax0_2_1.plot(x,y021, '-', color="black", )
##ax0_0_1.set_xlim(0,maxx)
##ax0_1_1.set_xlim(0,maxx)
###ax0_2_1.set_xlim(0,maxx)

##ax0_0_2 = plt.subplot(325)
##plt.title('$Y_n = f_0$, $Y_{n-1} = f_0$, $X_{n-1} = e_2$')
##ax0_0_2.hist(gamma_data002, _bins_big, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax0_0_2.plot(x,y002, '-', color="black", )
##ax0_1_2 = plt.subplot(326)
##plt.title('$Y_n = f_0$, $Y_{n-1} = f_1$, $X_{n-1} = e_2$')
##ax0_1_2.hist(gamma_data012, _bins_big, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax0_1_2.plot(x,y012, '-', color="black", )
##ax0_0_2.set_xlim(0,maxx)
##ax0_1_2.set_xlim(0,maxx)

##show()

##f = plt.figure(num=None, figsize=(10, 10), dpi=150, facecolor='w', edgecolor='k')
##subplots_adjust(hspace=0.4)

##plt.subplots_adjust(left=0.04, bottom=0.07, right=0.98, top=0.95, wspace=0.1)

##ax1_0_0 = plt.subplot(321)
##plt.title('$Y_n = f_1$, $Y_{n-1} = f_0$, $X_{n-1} = e_0$')
##ax1_0_0.hist(gamma_data100, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax1_0_0.plot(x,y100, '-', color="black", )
##ax1_1_0 = plt.subplot(322)
##plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_0$')
##ax1_1_0.hist(gamma_data110, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax1_1_0.plot(x,y110, '-', color="black", )
###ax1_2_0 = plt.subplot(333)
###plt.title('$Y_n = f_1$, $Y_{n-1} = f_2$, $X_{n-1} = e_0$')
###ax1_2_0.hist(gamma_data120, 100, normed=1, histtype='stepfilled', facecolor='white', alpha=0.75)
###ax1_2_0.plot(x,y120, '-', color="black", )
##ax1_0_0.set_xlim(0,maxx)
##ax1_1_0.set_xlim(0,maxx)
###ax1_2_0.set_xlim(0,maxx)

##ax1_0_1 = plt.subplot(323)
##plt.title('$Y_n = f_1$, $Y_{n-1} = f_0$, $X_{n-1} = e_1$')
##ax1_0_1.hist(gamma_data101, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax1_0_1.plot(x,y101, '-', color="black", )
##ax1_1_1 = plt.subplot(324)
##plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_1$')
##ax1_1_1.hist(gamma_data111, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax1_1_1.plot(x,y111, '-', color="black", )
###ax1_2_1 = plt.subplot(336)
###plt.title('$Y_n = f_1$, $Y_{n-1} = f_2$, $X_{n-1} = e_1$')
###ax1_2_1.hist(gamma_data121, 100, normed=1, histtype='stepfilled', facecolor='white', alpha=0.75)
###ax1_2_1.plot(x,y121, '-', color="black", )
##ax1_0_1.set_xlim(0,maxx)
##ax1_1_1.set_xlim(0,maxx)
###ax1_2_1.set_xlim(0,maxx)

##ax1_0_1 = plt.subplot(325)
##plt.title('$Y_n = f_1$, $Y_{n-1} = f_0$, $X_{n-1} = e_2$')
##ax1_0_1.hist(gamma_data102, _bins_big, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax1_0_1.plot(x,y102, '-', color="black", )
##ax1_1_1 = plt.subplot(326)
##plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_2$')
##ax1_1_1.hist(gamma_data112, _bins_big, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax1_1_1.plot(x,y112, '-', color="black", )
##ax1_0_1.set_xlim(0,maxx)
##ax1_1_1.set_xlim(0,maxx)

##show()

##f = plt.figure(num=None, figsize=(10, 10), dpi=150, facecolor='w', edgecolor='k')
##subplots_adjust(hspace=0.4)

##ax2_0_0 = plt.subplot(331)
##plt.title('$Y_n = f_2$, $Y_{n-1} = f_0$, $X_{n-1} = e_0$')
##ax2_0_0.hist(gamma_data200, 10, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75)
##ax2_0_0.plot(x,y200, '-', color="black", )
##ax2_1_0 = plt.subplot(332)
##plt.title('$Y_n = f_2$, $Y_{n-1} = f_1$, $X_{n-1} = e_0$')
##ax2_1_0.hist(gamma_data210, 10, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax2_1_0.plot(x,y210, '-', color="black", )
##ax2_0_0.set_xlim(0,maxx)
##ax2_1_0.set_xlim(0,maxx)

##ax2_1_1 = plt.subplot(335)
##plt.title('$Y_n = f_2$, $Y_{n-1} = f_1$, $X_{n-1} = e_1$')
##ax2_1_1.hist(gamma_data211, 10, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.75)
##ax2_1_1.plot(x,y211, '-', color="black", )
##ax2_1_1.set_xlim(0,maxx)

##show()


#subplots_adjust(hspace=0.15)
#subplots_adjust(wspace=0.1)
#ax0_0 = plt.subplot(221)
#plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
#ax0_0.hist(gamma_data000, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax0_0.plot(x,y000, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax0_0.hist(gamma_data001, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax0_0.plot(x,y001, '-', color="grey", alpha=0.85)

#ax0_0.hist(gamma_data002, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax0_0.plot(x,y002, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax0_0.set_xlim(0,maxx)
#ax0_0.set_xticks([0,0.8]);
#ax0_0.set_yticks([0,9]);

#ax0_1 = plt.subplot(222)
#plt.title('$Y_n = f_1$, $Y_{n-1} = f_2$, $X_{n-1} = e_i$')
#ax0_1.hist(gamma_data010, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax0_1.plot(x,y010, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax0_1.hist(gamma_data011, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax0_1.plot(x,y011, '-', color="grey", alpha=0.85)

#ax0_1.hist(gamma_data012, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax0_1.plot(x,y012, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax0_1.set_xlim(0,maxx)
#ax0_1.set_xticks([0,0.8]);
#ax0_1.set_yticks([0,9]);


#ax1_0 = plt.subplot(223)
#plt.title('$Y_n = f_2$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
#ax1_0.hist(gamma_data100, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax1_0.plot(x,y100, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax1_0.hist(gamma_data101, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax1_0.plot(x,y101, '-', color="grey", alpha=0.85)

#ax1_0.hist(gamma_data102, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax1_0.plot(x,y102, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax1_0.set_xlim(0,maxx)
#ax1_0.set_xticks([0,0.8]);
#ax1_0.set_yticks([0,16]);


#ax1_1 = plt.subplot(224)
#plt.title('$Y_n = f_2$, $Y_{n-1} = f_2$, $X_{n-1} = e_i$')
#ax1_1.hist(gamma_data110, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dashed', linewidth = 2.0)
#ax1_1.plot(x,y110, '-', color="black", linestyle = 'dashed', linewidth = 2.0)

#ax1_1.hist(gamma_data111, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
#ax1_1.plot(x,y111, '-', color="grey", alpha=0.85)

#ax1_1.hist(gamma_data112, _bins_big, normed=1, histtype='stepfilled', facecolor=(0, 0, 0, 0.1), edgecolor = (0.5, 0.5, 0.5, 0.75), linewidth = 3.0)
#ax1_1.plot(x,y112, '-', color="black", alpha=0.50, linewidth = 3.0)

#ax1_1.set_xlim(0,maxx)
#ax1_1.set_xticks([0,0.8]);
#ax1_1.set_yticks([0,16]);


##savefilename = u"D:/Наука/_Статьи/__в работе/2015 - BB-LNCS/pic3_hists.pdf";
##f.savefig(savefilename)
#show()


##ax1_1 = plt.subplot(224)
##plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_1$')
##ax1_1.hist(gamma_data110, _bins, normed=1, histtype='stepfilled', facecolor='black', alpha=0.75, linestyle = 'dotted')
##ax1_1.plot(x,y110, '-', color="black", linestyle = 'dotted')

##ax1_1.hist(gamma_data111, _bins, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.65)
##ax1_1.plot(x,y111, '-', color="grey", alpha=0.85)

##ax1_1.hist(gamma_data112, _bins_big, normed=1, histtype='stepfilled', facecolor='grey', alpha=0.50, linewidth = 3.0)
##ax1_1.plot(x,y112, '-', color="black", alpha=0.50, linewidth = 3.0)

##ax1_1.set_xlim(0,maxx)

