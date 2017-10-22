import matplotlib
import matplotlib.pyplot as plt
import numpy as np
import scipy.io as io
import matplotlib.tri as mtri
matplotlib.rc('text', usetex = True)
import pylab
import OutputParcerFunction



filename = "D:/?????/_??????/__? ??????/2015 - BB-LNCS/MSNetworkMonitor/CAPTools/x64/Debug/hangouts_2min_500166365.txt"
OffsetHost, OffsetSource, FrameNumber = np.loadtxt(filename, delimiter = ' ', usecols=(1,2,3), unpack=True, dtype=float)

OffsetHostThinned = np.ndarray(shape=(1,1), dtype = float); 
OffsetSourceThinned = np.ndarray(shape=(1,1), dtype = float); 
FrameNumberThinned = np.ndarray(shape=(1,1), dtype = float); 

OffsetHost0 = np.ndarray(shape=(1,1), dtype = float); 
OffsetSource0 = np.ndarray(shape=(1,1), dtype = float); 
FrameNumber0 = np.ndarray(shape=(1,1), dtype = float); 

OffsetHost1 = np.ndarray(shape=(1,1), dtype = float); 
OffsetSource1 = np.ndarray(shape=(1,1), dtype = float); 
FrameNumber1 = np.ndarray(shape=(1,1), dtype = float); 

OffsetHost2 = np.ndarray(shape=(1,1), dtype = float); 
OffsetSource2 = np.ndarray(shape=(1,1), dtype = float); 
FrameNumber2 = np.ndarray(shape=(1,1), dtype = float); 

OffsetHostThinned[0] = OffsetHost[0]
OffsetSourceThinned[0] = OffsetSource[0]
FrameNumberThinned[0] = FrameNumber[0]

OffsetHost0[0] = OffsetHost[0]
OffsetSource0[0] = OffsetSource[0]
FrameNumber0[0] = FrameNumber[0]
#FrameNumberShift = np.concatenate(([0], FrameNumber), axis = 0)
#FrameNumberAdj = np.concatenate((FrameNumber,[0]), axis = 0)

#FrameLoss = - FrameNumberShift + FrameNumberAdj
#FrameLoss[0] = 1
#FrameLoss.resize(FrameLoss.shape[0]-1)

#print(FrameLoss.shape[0]-1)
#print(FrameNumberShift )
#print(FrameNumberAdj)
#print(FrameLoss)


FrameNumberLast = 0;
for i in range(1, len(FrameNumber)):
    if (FrameNumber[i] > FrameNumberLast):
        OffsetHostThinned = np.append(OffsetHostThinned, OffsetHost[i])
        OffsetSourceThinned = np.append(OffsetSourceThinned, OffsetSource[i])
        FrameNumberThinned = np.append(FrameNumberThinned, FrameNumber[i])
        FrameNumberLast = FrameNumber[i]   
        #if (FrameNumber[i] - FrameNumber[i-1] == 1):
        #    OffsetHost0 = np.append(OffsetHost0, OffsetHost[i])
        #    OffsetSource0 = np.append(OffsetSource0, OffsetSource[i])
        #    FrameNumber0 = np.append(FrameNumber0, FrameNumber[i])
        #    print(OffsetHost[i], OffsetSource[i], FrameNumber[i], "NEXT")   
        #elif (FrameNumber[i] - FrameNumber[i-1] == 2):
        #    OffsetHost1 = np.append(OffsetHost1, OffsetHost[i])
        #    OffsetSource1 = np.append(OffsetSource1, OffsetSource[i])
        #    FrameNumber1 = np.append(FrameNumber1, FrameNumber[i])   
        #    print(OffsetHost[i], OffsetSource[i], FrameNumber[i], "LOST 1")   
        #else:
        #    OffsetHost2 = np.append(OffsetHost2, OffsetHost[i])
        #    OffsetSource2 = np.append(OffsetSource2, OffsetSource[i])
        #    FrameNumber2 = np.append(FrameNumber2, FrameNumber[i])   
        #    print(OffsetHost[i], OffsetSource[i], FrameNumber[i], "LOST 2")   

for i in range(1, len(FrameNumberThinned)):
    if (FrameNumberThinned[i] - FrameNumberThinned[i-1] == 1):
        OffsetHost0 = np.append(OffsetHost0, OffsetHostThinned[i])
        OffsetSource0 = np.append(OffsetSource0, OffsetSourceThinned[i])
        FrameNumber0 = np.append(FrameNumber0, FrameNumberThinned[i])
        print(OffsetHostThinned[i], OffsetSourceThinned[i], FrameNumberThinned[i], "NEXT")   
    elif (FrameNumberThinned[i] - FrameNumberThinned[i-1] == 2):
        OffsetHost1 = np.append(OffsetHost1, OffsetHostThinned[i])
        OffsetSource1 = np.append(OffsetSource1, OffsetSourceThinned[i])
        FrameNumber1 = np.append(FrameNumber1, FrameNumberThinned[i])   
        print(OffsetHostThinned[i], OffsetSourceThinned[i], FrameNumberThinned[i], "LOST 1")   
    else:
        OffsetHost2 = np.append(OffsetHost2, OffsetHostThinned[i])
        OffsetSource2 = np.append(OffsetSource2, OffsetSourceThinned[i])
        FrameNumber2 = np.append(FrameNumber2, FrameNumberThinned[i])   
        print(OffsetHostThinned[i], OffsetSourceThinned[i], FrameNumberThinned[i], "LOST 2")   

OffsetHost0Shf = np.concatenate(([0], OffsetHost0), axis = 0)
OffsetHost0Adj = np.concatenate((OffsetHost0,[0]), axis = 0)
OffsetHostDiff0 = - OffsetHost0Shf + OffsetHost0Adj
OffsetHostDiff0[0] = 0
OffsetHostDiff0.resize(OffsetHostDiff0.shape[0]-1)

OffsetSource0Shf = np.concatenate(([0], OffsetSource0), axis = 0)
OffsetSource0Adj = np.concatenate((OffsetSource0,[0]), axis = 0)
OffsetSourceDiff0 = - OffsetSource0Shf + OffsetSource0Adj
OffsetSourceDiff0[0] = 0
OffsetSourceDiff0.resize(OffsetSourceDiff0.shape[0]-1)

OffsetHostSourceDiff0 = -OffsetSource0 + OffsetHost0


onesOffsetHost1 = np.ones(OffsetHost1.shape)
onesOffsetHost2 = np.ones(OffsetHost2.shape)
pylab.figure();
n, bins, patches = pylab.hist(OffsetHostDiff0, 500, normed=1, histtype='stepfilled')
pylab.setp(patches, 'facecolor', 'g', 'alpha', 0.75)
pylab.ylim(0,5)
pylab.xlim(0,0.5)
#l = pylab.plot(bins, 'k--', linewidth=1.5)
pylab.show()


fig = plt.figure(num=None, figsize=(12, 4), dpi=150, facecolor='w', edgecolor='k')
ax = plt.subplot(111)

ax.plot(OffsetHost0,  OffsetHostDiff0, '-', color='grey', linewidth=1)
ax.plot(OffsetHost1, 0.16 * onesOffsetHost1, '.', color = 'black' )
ax.plot(OffsetHost2, 0.16 * onesOffsetHost2, 'x', color = 'black' )
ax.set_xlim(0,60.91)
plt.show()

fig = plt.figure(num=None, figsize=(12, 4), dpi=150, facecolor='w', edgecolor='k')
ax = plt.subplot(111)

ax.plot(OffsetSource0,  OffsetSourceDiff0, '-', color='grey', linewidth=1)
ax.plot(OffsetHost1, 0.16 * onesOffsetHost1, '.', color = 'black' )
ax.plot(OffsetHost2, 0.16 * onesOffsetHost2, 'x', color = 'black' )
ax.set_xlim(0,60.91)
plt.show()

fig = plt.figure(num=None, figsize=(12, 4), dpi=150, facecolor='w', edgecolor='k')
ax = plt.subplot(111)

ax.plot(OffsetHost0,  OffsetHostSourceDiff0, '-', color='grey', linewidth=1)
ax.plot(OffsetHost1, 0.16 * onesOffsetHost1, '.', color = 'black' )
ax.plot(OffsetHost2, 0.16 * onesOffsetHost2, 'x', color = 'black' )
ax.set_xlim(0,60.91)
plt.show()

        #print(OffsetHostThinned)
        #print(OffsetSourceThinned)
        #print(FrameNumberThinned)
        #print(OffsetHost[i], OffsetSource[i], FrameNumber[i])
    #else:
        #print(OffsetHost[i], OffsetSource[i], FrameNumber[i], "DISCARDED")

#print(OffsetHostThinned.shape)
#print(OffsetSourceThinned.shape)
#print(FrameNumberThinned.shape)

#print(OffsetHostThinned)
#print(OffsetSourceThinned)
#print(FrameNumberThinned)

