# 정규화
from sklearn.preprocessing import MinMaxScaler
import pandas as pd
import numpy as np
import math

def csv_normalize(file, id):
    scaler = MinMaxScaler()
    df = pd.read_csv(file)
    df1 = df.drop_duplicates(['Unnamed: 0'], keep='first')
    df1 = df1.drop(index=0)
    # df1['label']= 6
    # dfD = df1.loc[:,['delta']]
    # print(df1.loc[:,['delta','theta','lowAlpha','highAlpha','lowBeta','highBeta','lowGamma','highGamma']])
    df1 = df1.loc[:, 'delta':'highGamma']  # numarr
    # print(df1)
    df1 = np.ravel(df1, order='F')
    # print(df1)
    df1 = np.asarray(df1)

    numarr = []
    print(df1.size)
    collen = int(df1.size / 8)
    for i in range(df1.shape[0]):
        numarr.append(i + 1)

    # print(numarr)
    # df2 = np.concatenate((numarr, df1), axis = 0)
    df2 = np.vstack([numarr, df1])

    df2 = df2.swapaxes(0, 1)
    df2 = scaler.fit_transform(df2)
    # print(df2)
    df2 = np.delete(df2, 0, axis=1)
    # print(df2)

    df2 = df2.reshape(8, collen)
    df2 = df2.swapaxes(0, 1)

    # print(df2)
    for i in range(df2.shape[0]):
        for j in range(df2.shape[1]):
            df2[i][j] = math.floor(df2[i][j] * 10000000)
    np.savetxt(id + ".csv", df2, fmt='%d', delimiter=",")

#df1.to_csv('test1.csv')

# new_colnames = [i+'_mms' for i in colnames]
# pd.concat([df, pd.DataFrame(scaler.fit_transform(df[colnames]), columns=new_colnames)], axis=1)
