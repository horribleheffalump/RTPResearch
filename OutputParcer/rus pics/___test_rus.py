 # -*- coding: utf-8 -*-
from __future__ import (absolute_import, division, print_function, unicode_literals)

#from matplotlib.externals import six
#import matplotlib.pyplot as plt
#import numpy as np
#import math

#from matplotlib import rc
#rc('font', family='serif')
#rc('text', usetex=True)
#rc('text.latex',unicode=True)
#rc('text.latex',preamble=r'\usepackage[utf8]{inputenc}')
#rc('text.latex',preamble=r'\usepackage[russian]{babel}')


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
import math

def figsize(wcm,hcm): plt.figure(figsize=(wcm/2.54,hcm/2.54))
figsize(13,9)

x = np.linspace(0,2*math.pi,100)
y = np.sin(x)
plt.plot(x,y,'-')
plt.xlabel(u"Ось абсцисс")
#plt.show()

plt.savefig(u"d:/fig.pdf")
