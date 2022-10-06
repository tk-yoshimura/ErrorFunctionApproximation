import numpy as np
import pandas as pd
import matplotlib.pyplot as plt

dirpath_summary = '../results/'
suffix_summary = '.csv'

dirpath_figure = '../figures/'
suffix_figure = '.svg'

targets = [
    'erf_approx',
    'erfc_approx',
    'inverf_approx',
    'inverfc_approx',
]

for target in targets:
    data = pd.read_csv(dirpath_summary + target + suffix_summary) 

    x, y, err = data['x'], data['y_actual'], data['error(rate)']
    err = np.where(np.abs(y) < 1e-305, 0, err)

    plt.clf()
    fig = plt.figure(figsize=(12, 6))

    ax1 = fig.add_subplot(2, 1, 1)
    ax2 = fig.add_subplot(2, 1, 2)

    ax1.plot(x, y)
    ax1.grid()
    ax1.set_ylabel('actual')

    ax2.plot(x, err)
    ax2.grid()
    ax2.set_ylabel('error(rate)')

    plt.savefig(dirpath_figure + target + suffix_figure, bbox_inches='tight')
