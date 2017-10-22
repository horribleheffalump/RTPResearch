# -*- coding: utf-8 -*-
import matplotlib.pyplot as plt
import numpy as np
import math

from matplotlib import rc
#rc('font',**{'family':'verdana'})
rc('text', usetex=True)
rc('text.latex',unicode=True)
rc('text.latex',preamble=r'\usepackage[utf8]{inputenc}')
rc('text.latex',preamble=r'\usepackage[T1]{fontenc}')
rc('text.latex',preamble=r'\usepackage[russian]{babel}')

from matplotlib.backends.backend_pdf import PdfPages

def figsize(wcm,hcm): plt.figure(figsize=(wcm/2.54,hcm/2.54))

with PdfPages('page.pdf') as pdf:
    figsize(13,9)

    x = np.linspace(0,2*math.pi,100)
    y = np.sin(x)
    plt.plot(x,y,'-')
    str = u"Ось абсцисс"
    plt.xlabel(str)
    #plt.xlabel("The axis label")
    pdf.savefig(bbox_inches='tight')
    #plt.show()