# -*- coding: utf-8 -*-
import matplotlib.pyplot as plt
import numpy as np
import math

from matplotlib import rc
rc('font',**{'family':'serif'})
rc('text', usetex=True)
rc('text.latex',unicode=True)
rc('text.latex',preamble=r'\usepackage[T2A]{fontenc}')
rc('text.latex',preamble=r'\usepackage[utf8]{inputenc}')
rc('text.latex',preamble=r'\usepackage[russian]{babel}')

def figsize(wcm,hcm): plt.figure(figsize=(wcm/2.54,hcm/2.54))
figsize(13,9)

x = np.linspace(0,2*math.pi,100)
y = np.sin(x)
plt.plot(x,y,'-')
plt.xlabel(u"Ось абсцисс")
plt.savefig(u"d:/fig1.pdf")
#plt.show()