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

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_0/0_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t000,a000,b000 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_0/0_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t010,a010,b010 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_0/0_2_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t020,a020,b020 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

#==== 0 1 ======

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_1/0_0_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t001,a001,b001 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_1/0_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t011,a011,b011 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_1/0_2_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t021,a021,b021 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

#==== 0 2 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_2/0_0_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t002,a002,b002 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_2/0_1_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t012,a012,b012 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/0_2/0_2_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t022,a022,b022 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

#==== 1 0 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_0/1_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t100,a100,b100 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_0/1_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t110,a110,b110 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_0/1_2_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t120,a120,b120 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

#==== 1 1 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_1/1_0_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t101,a101,b101 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_1/1_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t111,a111,b111 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_1/1_2_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t121,a121,b121 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

#==== 1 2 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_2/1_0_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t102,a102,b102 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_2/1_1_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t112,a112,b112 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/1_2/1_2_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t122,a122,b122 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)


#==== 2 0 ======
filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_0/2_0_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t200,a200,b200 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_0/2_1_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t210,a210,b210 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_0/2_2_0_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t220,a220,b220 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

#==== 2 1 ======

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_1/2_0_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t201,a201,b201 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_1/2_1_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t211,a211,b211 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_1/2_2_1_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t221,a221,b221 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

#==== 2 2 ======

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_2/2_0_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t202,a202,b202 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_2/2_1_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t212,a212,b212 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)

filename = u"../Data/data 2015-12-7 21-29 - 3g 20min/2_2/2_2_2_gamma_params.txt" #Estimate_1 0.01, Estimate_2 0.001  
t222,a222,b222 = np.loadtxt(filename, delimiter = ' ', usecols=(0,1,2), unpack=True, dtype=float)


from pylab import *

f = plt.figure(num=None, figsize=(10, 5.5), dpi=150, facecolor='w', edgecolor='k')
#subplots_adjust(hspace=0.0)

plt.subplots_adjust(left=0.04, bottom=0.04, right=0.98, top=0.95)


subplots_adjust(hspace=0.15)
subplots_adjust(wspace=0.1)
ax0_0 = plt.subplot(331)
plt.title('$Y_n = f_1$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
ax0_0.plot(t000,a000, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax0_0.plot(t000,b000, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax0_0.plot(t001,a001, '-', color="black", alpha=0.85)
ax0_0.plot(t001,b001, '-', color="black", alpha=0.85)
ax0_0.plot(t002,a002, '-', color="black", alpha=0.50, linewidth = 3.0)
ax0_0.plot(t002,b002, '-', color="black", alpha=0.50, linewidth = 3.0)
ax0_0.set_ylim(0,10)
#ax0_0.set_xlim(400,1200)
ax0_0.set_xticks([0]);
ax0_0.set_yticks([0,10]);

ax0_1 = plt.subplot(332)
plt.title('$Y_n = f_1$, $Y_{n-1} = f_2$, $X_{n-1} = e_i$')
ax0_1.plot(t010,a010, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax0_1.plot(t010,b010, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax0_1.plot(t011,a011, '-', color="black", alpha=0.85)
ax0_1.plot(t011,b011, '-', color="black", alpha=0.85)
ax0_1.plot(t012,a012, '-', color="black", alpha=0.50, linewidth = 3.0)
ax0_1.plot(t012,b012, '-', color="black", alpha=0.50, linewidth = 3.0)
ax0_1.set_ylim(0,10)
#x0_1.set_xlim(400,1200)
ax0_1.set_xticks([0]);
ax0_1.set_yticks([0,10]);

ax0_2 = plt.subplot(333)
plt.title('$Y_n = f_1$, $Y_{n-1} = f_3$, $X_{n-1} = e_i$')
ax0_2.plot(t020,a020, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax0_2.plot(t020,b020, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax0_2.plot(t021,a021, '-', color="black", alpha=0.85)
ax0_2.plot(t021,b021, '-', color="black", alpha=0.85)
ax0_2.plot(t022,a022, '-', color="black", alpha=0.50, linewidth = 3.0)
ax0_2.plot(t022,b022, '-', color="black", alpha=0.50, linewidth = 3.0)
ax0_2.set_ylim(0,10)
#ax0_2.set_xlim(0,maxx)
ax0_2.set_xticks([0]);
ax0_2.set_yticks([0,10]);


ax1_0 = plt.subplot(334)
plt.title('$Y_n = f_2$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
ax1_0.plot(t100,a100, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax1_0.plot(t100,b100, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax1_0.plot(t101,a101, '-', color="black", alpha=0.85)
ax1_0.plot(t101,b101, '-', color="black", alpha=0.85)
ax1_0.plot(t102,a102, '-', color="black", alpha=0.50, linewidth = 3.0)
ax1_0.plot(t102,b102, '-', color="black", alpha=0.50, linewidth = 3.0)
ax1_0.set_ylim(0,1)
#ax1_0.set_xlim(0,maxx)
ax1_0.set_xticks([0]);
ax1_0.set_yticks([0,10]);


ax1_1 = plt.subplot(335)
plt.title('$Y_n = f_2$, $Y_{n-1} = f_2$, $X_{n-1} = e_i$')
ax1_1.plot(t110,a110, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax1_1.plot(t110,b110, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax1_1.plot(t111,a111, '-', color="black", alpha=0.85)
ax1_1.plot(t111,b111, '-', color="black", alpha=0.85)
ax1_1.plot(t112,a112, '-', color="black", alpha=0.50, linewidth = 3.0)
ax1_1.plot(t112,b112, '-', color="black", alpha=0.50, linewidth = 3.0)
ax1_1.set_ylim(0,1)
#ax1_1.set_xlim(0,maxx)
ax1_1.set_xticks([0]);
ax1_1.set_yticks([0,10]);


ax1_2 = plt.subplot(336)
plt.title('$Y_n = f_2$, $Y_{n-1} = f_3$, $X_{n-1} = e_i$')
ax1_2.plot(t120,a120, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax1_2.plot(t120,b120, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax1_2.plot(t121,a121, '-', color="black", alpha=0.85)
ax1_2.plot(t121,b121, '-', color="black", alpha=0.85)
ax1_2.plot(t122,a122, '-', color="black", alpha=0.50, linewidth = 3.0)
ax1_2.plot(t122,b122, '-', color="black", alpha=0.50, linewidth = 3.0)
ax1_2.set_ylim(0,1)
#ax1_2.set_xlim(0,maxx)
ax1_2.set_xticks([0]);
ax1_2.set_yticks([0,10]);



ax2_0 = plt.subplot(337)
plt.title('$Y_n = f_3$, $Y_{n-1} = f_1$, $X_{n-1} = e_i$')
ax2_0.plot(t200,a200, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax2_0.plot(t200,b200, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax2_0.plot(t201,a201, '-', color="black", alpha=0.85)
ax2_0.plot(t201,b201, '-', color="black", alpha=0.85)
ax2_0.plot(t202,a202, '-', color="black", alpha=0.50, linewidth = 3.0)
ax2_0.plot(t202,b202, '-', color="black", alpha=0.50, linewidth = 3.0)
ax2_0.set_ylim(0,1)
#ax2_0.set_xlim(0,maxx)
ax2_0.set_xticks([0]);
ax2_0.set_yticks([0,10]);


ax2_1 = plt.subplot(338)
plt.title('$Y_n = f_3$, $Y_{n-1} = f_2$, $X_{n-1} = e_i$')
ax2_1.plot(t210,a210, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax2_1.plot(t210,b210, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax2_1.plot(t211,a211, '-', color="black", alpha=0.85)
ax2_1.plot(t211,b211, '-', color="black", alpha=0.85)
ax2_1.plot(t212,a212, '-', color="black", alpha=0.50, linewidth = 3.0)
ax2_1.plot(t212,b212, '-', color="black", alpha=0.50, linewidth = 3.0)
ax2_1.set_ylim(0,1)
#ax2_1.set_xlim(0,maxx)
ax2_1.set_xticks([0]);
ax2_1.set_yticks([0,10]);


ax2_2 = plt.subplot(339)
plt.title('$Y_n = f_3$, $Y_{n-1} = f_3$, $X_{n-1} = e_i$')
ax2_2.plot(t220,a220, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax2_2.plot(t220,b220, '-', color="black", linestyle = 'dashed', linewidth = 2.0)
ax2_2.plot(t221,a221, '-', color="black", alpha=0.85)
ax2_2.plot(t221,b221, '-', color="black", alpha=0.85)
ax2_2.plot(t222,a222, '-', color="black", alpha=0.50, linewidth = 3.0)
ax2_2.plot(t222,b222, '-', color="black", alpha=0.50, linewidth = 3.0)
ax2_2.set_ylim(0,1)
#ax2_2.set_xlim(0,maxx)
ax2_2.set_xticks([0]);
ax2_2.set_yticks([0,10]);

savefilename = u"D:/Наука/_Статьи/__в работе/2015 - new2an - rus/pic_gamma_params.pdf";
f.savefig(savefilename)
#show()

