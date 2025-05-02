import pandas as pd
import numpy as np
import warnings
warnings.filterwarnings('ignore')

df_pi = pd.read_excel('데이터톤 투수 최종 데이터.xlsx')
df_ba = pd.read_excel('데이터톤 야수 최종 데이터.xlsx')

# 원본 데이터 보존을 위한 카피
df_pi_m1 = df_pi.copy()
df_ba_m1 = df_ba.copy()

# 일단 필요한 칼럼만 따오기
df_pi_m1 = df_pi_m1[['구단명', '선수명', '포지션', 'Age']]
df_ba_m1 = df_ba_m1[['구단명', '선수명', '포지션', 'Age', 'oWAR', 'dWAR', 'FA 계약 연수', 'FA 계약 총액', '잔류 여부']]

df_ba_m1['종합 WAR'] = df_ba_m1['oWAR'] + df_ba_m1['dWAR']
df_ba_m1 = df_ba_m1.drop(['oWAR', 'dWAR'], axis=1)

# 4년 단위로 선수 성적 합산
df_pi_m1['group'] = df_pi_m1.groupby('선수명').cumcount() // 4
df_ba_m1['group'] = df_ba_m1.groupby('선수명').cumcount() // 4

df_pi_m1['FA 선언 시점 연령'] = df_pi_m1.groupby(['선수명', 'group'])['Age'].transform(lambda x: x.iloc[-1])
df_ba_m1['FA 선언 시점 연령'] = df_ba_m1.groupby(['선수명', 'group'])['Age'].transform(lambda x: x.iloc[-1])

df_pi_m1 = df_pi_m1.drop('Age', axis=1)
df_ba_m1 = df_ba_m1.drop('Age', axis=1)

df_pi_m1 = df_pi_m1.groupby(['선수명', 'group']).agg({
    '포지션': lambda x: x.mode().iloc[0],
    '잔류 여부': lambda x: x.mode().iloc[0],
    '종합 WAR': 'sum',
    'FA 계약 연수': 'sum',
    'FA 계약 총액': 'sum',
    'FA 선언 시점 연령': 'max'
}).reset_index()

df_ba_m1 = df_ba_m1.groupby(['선수명', 'group']).agg({
    '포지션': lambda x: x.mode().iloc[0],
    '잔류 여부': lambda x: x.mode().iloc[0],
    '종합 WAR': 'sum',
    'FA 계약 연수': 'sum',
    'FA 계약 총액': 'sum',
    'FA 선언 시점 연령': 'max'
}).reset_index()

# 필요 없는 칼럼 제거
df_pi_m1 = df_pi_m1.drop('group', axis=1)
df_ba_m1 = df_ba_m1.drop('group', axis=1)

# 해외 이적 선수는 kbo 안에서 계약한 것이 아니고 은퇴 선수는 계약 내역이 없어 이적/잔류 케이스만 추출
df_pi_m1 = df_pi_m1[df_pi_m1['잔류 여부'].isin(['이적', '잔류'])]
df_ba_m1 = df_ba_m1[df_ba_m1['잔류 여부'].isin(['이적', '잔류'])]

# 공통 속성 칼럼만 남아 있으므로 양 데이터 통합
df_all_m1 = pd.concat([df_pi_m1, df_ba_m1])


# 테스트 데이터에도 동일한 처리 진행

df_pi_25 = pd.read_excel('2025 KBO 투수 FA.xlsx')
df_ba_25 = pd.read_excel('2025 KBO 야수 FA.xlsx')

df_pi_25 = df_pi_25[['구단명', '선수명', '포지션', 'Age', '종합 WAR', 'FA 계약 연수', 'FA 계약 총액', '잔류 여부']]
df_ba_25 = df_ba_25[['구단명', '선수명', '포지션', 'Age', 'oWAR', 'dWAR', 'FA 계약 연수', 'FA 계약 총액', '잔류 여부']]

df_ba_25['종합 WAR'] = df_ba_25['oWAR'] + df_ba_25['dWAR']
df_ba_25 = df_ba_25.drop(['oWAR', 'dWAR'], axis=1)

df_pi_25['group'] = df_pi_25.groupby('선수명').cumcount() // 4
df_ba_25['group'] = df_ba_25.groupby('선수명').cumcount() // 4

df_pi_25['FA 선언 시점 연령'] = df_pi_25.groupby(['선수명', 'group'])['Age'].transform(lambda x: x.iloc[-1])
df_ba_25['FA 선언 시점 연령'] = df_ba_25.groupby(['선수명', 'group'])['Age'].transform(lambda x: x.iloc[-1])

df_pi_25 = df_pi_25.drop('Age', axis=1)
df_ba_25 = df_ba_25.drop('Age', axis=1)

df_pi_25 = df_pi_25.groupby(['선수명', 'group']).agg({
    '포지션': lambda x: x.mode().iloc[0],
    '잔류 여부': lambda x: x.mode().iloc[0],
    '종합 WAR': 'sum',
    'FA 계약 연수': 'sum',
    'FA 계약 총액': 'sum',
    'FA 선언 시점 연령': 'max'
}).reset_index()

df_ba_25 = df_ba_25.groupby(['선수명', 'group']).agg({
    '포지션': lambda x: x.mode().iloc[0],
    '잔류 여부': lambda x: x.mode().iloc[0],
    '종합 WAR': 'sum',
    'FA 계약 연수': 'sum',
    'FA 계약 총액': 'sum',
    'FA 선언 시점 연령': 'max'
}).reset_index()

# 해외 이적 선수는 kbo 안에서 계약한 것이 아니고 은퇴 선수는 계약 내역이 없어 이적/잔류 케이스만 추출
df_pi_25 = df_pi_25[df_pi_25['잔류 여부'].isin(['이적', '잔류'])]
df_ba_25 = df_ba_25[df_ba_25['잔류 여부'].isin(['이적', '잔류'])]

df_all_25 = pd.concat([df_pi_25, df_ba_25])

# 칼럼 순서를 재정렬
df_all_m1 = df_all_m1[['선수명', '포지션', '종합 WAR', 'FA 선언 시점 연령', '잔류 여부', 'FA 계약 연수', 'FA 계약 총액']]
df_all_25 = df_all_25[['선수명', '포지션', '종합 WAR', 'FA 선언 시점 연령', '잔류 여부', 'FA 계약 연수', 'FA 계약 총액']]

# 선수명은 계약 결과와 무관하므로 따로 임시 보관
names_1 = df_all_m1['선수명']
names_25 = df_all_25['선수명']

df_all_m1 = df_all_m1.drop('선수명', axis=1)
df_all_25 = df_all_25.drop('선수명', axis=1)

# 원핫 인코딩으로 포지션과 잔류 여부 인코딩
# 문자형 칼럼인 포지션과 잔류 여부만 골라 인코딩
columns = df_all_m1.select_dtypes(include='object').columns

data = pd.concat([df_all_m1, df_all_25])
data_oh = pd.get_dummies(data)

df_all_m1 = data_oh.iloc[:len(df_all_m1)]
df_all_25 = data_oh.iloc[len(df_all_m1):]



# 훈련용-검증용 데이터 분할(2013~2023)
from sklearn.model_selection import train_test_split

target_m1 = df_all_m1[['FA 계약 연수', 'FA 계약 총액']]
target_25 = df_all_25[['FA 계약 연수', 'FA 계약 총액']]

df_all_m1 = df_all_m1.drop(['FA 계약 연수', 'FA 계약 총액'], axis=1)
df_all_25 = df_all_25.drop(['FA 계약 연수', 'FA 계약 총액'], axis=1)

x_train, x_val, y_train, y_val = train_test_split(df_all_m1, target_m1, test_size=0.2, random_state=0)

# 앞서 종속변수인 계약 연수와 계약 금액과는 선형적인 상관관계가 존재함을 확인한 바 있음
# 단순한 회귀모델은 개별 종속변수에 독립적인 회귀분석을 진행하므로 결과가 의도했던 바와 달라질 위험이 큼
# 요소 사이의 관계를 고려할 수 있는 딥러닝 기법 사용

import tensorflow as tf
from tensorflow.keras import *

input_dim = x_train.shape[1]  # 입력 차원은 칼럼 수, 출력 차원은 종속변수 개수
model = models.Sequential([
    layers.Input(shape=(input_dim, )),
    layers.Dense(64, activation='relu'),
    layers.BatchNormalization(),
    layers.Dense(32, activation='relu'),
    layers.Dense(2)
])

model.compile(optimizer='adam', loss='mse')
model.fit(x_train, y_train, epochs=50, batch_size=16, validation_data=(x_val, y_val))
model.evaluate(df_all_25, target_25)

y_pred_25 = model.predict(df_all_25)

comparison = pd.DataFrame({
    '선수명' : names_25,
    '예측 연수': y_pred_25[:, 0],
    '예측 총액': y_pred_25[:, 1],
    '실제 연수': target_25['FA 계약 연수'].values,
    '실제 총액': target_25['FA 계약 총액'].values
})

from sklearn.metrics import r2_score

r2_y1 = r2_score(target_25.iloc[:, 0], y_pred_25[:, 0])  # 계약 연수
r2_y2 = r2_score(target_25.iloc[:, 1], y_pred_25[:, 1])  # 계약 총액