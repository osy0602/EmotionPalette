import cv2
import mediapipe as mp
from PIL import ImageFont, ImageDraw, Image
import numpy as np
import socket
import argparse
import time
from datetime import datetime
import firebase_admin
from firebase_admin import credentials
from firebase_admin import db
from uuid import uuid4
from firebase_admin import storage
import struct
import json
import normalize


#<UDP,TCP 실행>
#접속할 서버 주소, 인터페이스 주소 = localhost 사용
IP = "127.0.0.1"
UDP_PORT1 = 7777
UDP_PORT2 = 8888
#클라이언트 접속을 대기하는 포트번호
TCP_PORT = 8000

UDP_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
TCP_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
TCP_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
TCP_socket.bind((IP, TCP_PORT))
TCP_socket.listen()

#accept 함수에서 대기하다가 클라이언트가 접속하면 새로운 소켓을 리턴
client_socket, addr =TCP_socket.accept()
#접속한 클라이언트의 주소
print('Connected by', addr)

last = []
#personal_ID = "eunjju1"
receivedData_from_Unity = False

#<Firebase 연결>

db_url = 'https://emotion-palette-default-rtdb.firebaseio.com/'
cred = credentials.Certificate("C:/emotion_palette_firebase.JSON")
default_app = firebase_admin.initialize_app(cred, {'databaseURL':db_url ,'storageBucket':'emotion-palette.appspot.com'})
# firebase_Sto = firebase_admin.initialize_app(cred, {
#     'storageBucket':'emotion-palette.appspot.com'})
#<Mediapipe로 동작인식 시작>
mp_drawing = mp.solutions.drawing_utils
mp_hands = mp.solutions.hands
mp_drawing_styles = mp.solutions.drawing_styles
count=0
shoot = False
# For webcam inpu
cap = cv2.VideoCapture(0)
k = 0

try:
    while True:
        # 유니티에서 ID 받아오기
        receivedData = client_socket.recv(1024).decode("UTF-8")
        personal_ID = receivedData
        print("유니티에서 받아온 체험자의 ID :", personal_ID)
        receivedData_from_Unity = True

        if (receivedData_from_Unity == True):


            # upload file


            # eunjju1 컬러칩 값만 가져옴
            #ref = db.reference().child('ID').child(personal_ID).child('colorlist')
            ref = db.reference().child(personal_ID).child('colorlist')
            colorlist = ref.get()

            # 컬러칩 str로 형변환
            color1 = str(colorlist[0])
            color2 = str(colorlist[1])
            color3 = str(colorlist[2])
            color4 = str(colorlist[3])
            color5 = str(colorlist[4])
            color6 = str(colorlist[5])
            color7 = str(colorlist[6])
            color8 = str(colorlist[7])
            color9 = str(colorlist[8])
            color10 = str(colorlist[9])
            print('체험자의 컬러칩 결과 : ', colorlist)

            # eunjju1 감정 유사율 퍼센티지
            ref = db.reference().child(personal_ID).child('emotion_all')
            emotion_all = ref.get()
            emotion_list = []
            emotion_percent = []
            for i in range(len(emotion_all)):
                emotion_list.append(emotion_all[i][0])
                emotion_percent.append(emotion_all[i][1])

            dictionary = dict(zip(emotion_list, emotion_percent))

            DynamicPer = str(int(dictionary['Dynamic']))
            ElegantPer = str(int(dictionary['Elegant']))
            RomanticPer = str(int(dictionary['Romantic']))
            CasualPer = str(int(dictionary['Casual']))
            EroticPer = str(int(dictionary['Erotic']))
            NaturalPer = str(int(dictionary['Natural']))
            # DynamicPer + " " + ElegantPer + " "+ RomanticPer + " "+ CasualPer + " "+ EroticPer + " "+ NaturalPer + " "

            # eunjju1 컬러칩 값만 가져옴
            ref = db.reference().child(personal_ID).child('emotion_top3_color')
            top3 = ref.get()

            # top3 감정만 뽑아냄
            main_emo = top3[0][0]
            sub1_emo = top3[1][0]
            sub2_emo = top3[2][0]
            Top3_emo = [main_emo, sub1_emo, sub2_emo]
            print('체험자의 TOP3 감정 : ', Top3_emo)

            # Top3 감정 str로 형변환
            emo1 = str(main_emo)
            emo2 = str(sub1_emo)
            emo3 = str(sub2_emo)

            # 유사율 수치만 뽑아냄
            main_rate = top3[0][1]
            sub1_rate = top3[1][1]
            sub2_rate = top3[2][1]
            # main_rate = 4
            # sub1_rate = 3
            # sub2_rate = 3
            emotion_rate = [main_rate, sub1_rate, sub2_rate]
            print('체험자의 TOP3 감정 유사율 비율 : ', emotion_rate)

            # 유사율str로 형변환
            main = str(main_rate)
            sub1 = str(sub1_rate)
            sub2 = str(sub2_rate)

            # str 하나의 문자열로 합치기
            emotion_values = " " + color1 + " " + color2 + " " + color3 + " " + color4 + " " + color5 + " " + color6 + " " + color7 + " " + color8 + " " + color9 + " " + color10 + " " + main + " " + sub1 + " " + sub2 + " " + emo1 + " " + emo2 + " " + emo3 + " " + DynamicPer + " " + ElegantPer + " " + RomanticPer + " " + CasualPer + " " + EroticPer + " " + NaturalPer + " "
            # emotion_values = " " + color1 + " " + color2 + " " + color3 + " " + color4 + " " + color5 + " " + color6 + " " + color7 + " " + color8 + " " + color9 + " " + color10 + " " + main + " " + sub1 + " " + sub2 + " " + emo1 + " " + emo2 + " " + emo3 + " "
            print(emotion_values)

            # msg = "TCP initialized "
            msg = 'emotion_values' + emotion_values
            client_socket.sendall(msg.encode())
            # print('send 완료 ' + str(k))
            print('감정 분석 결과 전송 완료')
            receivedData_from_Unity = False

    #         default_app = firebase_admin.initialize_app(cred, {
    # 'storageBucket':'emotion-palette.appspot.com'})


            bucket = storage.bucket()
            blob = bucket.blob('D:/exhibition/'+ personal_ID +'.csv')
            # new token and metadata 설정
            new_token = uuid4()
            metadata = {"firebaseStorageDownloadTokens": new_token}  # access token이 필요하다.
            blob.metadata = metadata

            # upload file

            blob.download_to_filename(personal_ID +'.csv')  # 여기에 받아서 내 컴퓨터에 저장할 이름 쓰기
            print(blob.public_url)
            # upload file
            normalize.csv_normalize(personal_ID +'.csv', personal_ID) #normalize 함수 실행 후 ID.csv로 저장해줌
            break


except:  # 접속이 끊기면 except가 발생한다.
    print("except : ", addr)

finally:
    client_socket.close()

with mp_hands.Hands(
        min_detection_confidence=0.5,
        min_tracking_confidence=0.5) as hands:
    while cap.isOpened():
        success, image = cap.read()
        if not success:
            print("카메라를 찾을 수 없습니다.")

            # If loading a video, use 'break' instead of 'continue'.
            continue

        # Flip the image horizontally for a later selfie-view display, and convert
        # the BGR image to RGB.
        image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)

        # To improve performance, optionally mark the image as not writeable to
        # pass by reference.
        counts = 1
        image.flags.writeable = False
        results = hands.process(image)

        # Draw the hand annotations on the image.
        image.flags.writeable = True
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
        image_height, image_width, _ = image.shape

        if results.multi_hand_landmarks and results.multi_handedness[0].classification[0].label == "Right":
            for hand_landmarks in results.multi_hand_landmarks:
                shoulder_points = hand_landmarks.landmark[0].y

                # 엄지를 제외한 나머지 4개 손가락의 마디 위치 관계를 확인하여 플래그 변수를 설정합니다. 손가락을 일자로 편 상태인지 확인합니다.
                thumb_finger_state = 0
                if hand_landmarks.landmark[mp_hands.HandLandmark.THUMB_CMC].y * image_height > hand_landmarks.landmark[
                    mp_hands.HandLandmark.THUMB_MCP].y * image_height:
                    if hand_landmarks.landmark[mp_hands.HandLandmark.THUMB_MCP].y * image_height > \
                            hand_landmarks.landmark[mp_hands.HandLandmark.THUMB_IP].y * image_height:
                        if hand_landmarks.landmark[mp_hands.HandLandmark.THUMB_IP].y * image_height > \
                                hand_landmarks.landmark[mp_hands.HandLandmark.THUMB_TIP].y * image_height:
                            thumb_finger_state = 1

                index_finger_state = 0
                if hand_landmarks.landmark[mp_hands.HandLandmark.INDEX_FINGER_MCP].y * image_height > \
                        hand_landmarks.landmark[mp_hands.HandLandmark.INDEX_FINGER_PIP].y * image_height:
                    if hand_landmarks.landmark[mp_hands.HandLandmark.INDEX_FINGER_PIP].y * image_height > \
                            hand_landmarks.landmark[mp_hands.HandLandmark.INDEX_FINGER_DIP].y * image_height:
                        if hand_landmarks.landmark[mp_hands.HandLandmark.INDEX_FINGER_DIP].y * image_height > \
                                hand_landmarks.landmark[mp_hands.HandLandmark.INDEX_FINGER_TIP].y * image_height:
                            index_finger_state = 1

                middle_finger_state = 0
                if hand_landmarks.landmark[mp_hands.HandLandmark.MIDDLE_FINGER_MCP].y * image_height > \
                        hand_landmarks.landmark[mp_hands.HandLandmark.MIDDLE_FINGER_PIP].y * image_height:
                    if hand_landmarks.landmark[mp_hands.HandLandmark.MIDDLE_FINGER_PIP].y * image_height > \
                            hand_landmarks.landmark[mp_hands.HandLandmark.MIDDLE_FINGER_DIP].y * image_height:
                        if hand_landmarks.landmark[mp_hands.HandLandmark.MIDDLE_FINGER_DIP].y * image_height > \
                                hand_landmarks.landmark[mp_hands.HandLandmark.MIDDLE_FINGER_TIP].y * image_height:
                            middle_finger_state = 1

                ring_finger_state = 0
                if hand_landmarks.landmark[mp_hands.HandLandmark.RING_FINGER_MCP].y * image_height > \
                        hand_landmarks.landmark[mp_hands.HandLandmark.RING_FINGER_PIP].y * image_height:
                    if hand_landmarks.landmark[mp_hands.HandLandmark.RING_FINGER_PIP].y * image_height > \
                            hand_landmarks.landmark[mp_hands.HandLandmark.RING_FINGER_DIP].y * image_height:
                        if hand_landmarks.landmark[mp_hands.HandLandmark.RING_FINGER_DIP].y * image_height > \
                                hand_landmarks.landmark[mp_hands.HandLandmark.RING_FINGER_TIP].y * image_height:
                            ring_finger_state = 1

                pinky_finger_state = 0
                if hand_landmarks.landmark[mp_hands.HandLandmark.PINKY_MCP].y * image_height > hand_landmarks.landmark[
                    mp_hands.HandLandmark.PINKY_PIP].y * image_height:
                    if hand_landmarks.landmark[mp_hands.HandLandmark.PINKY_PIP].y * image_height > \
                            hand_landmarks.landmark[mp_hands.HandLandmark.PINKY_DIP].y * image_height:
                        if hand_landmarks.landmark[mp_hands.HandLandmark.PINKY_DIP].y * image_height > \
                                hand_landmarks.landmark[mp_hands.HandLandmark.PINKY_TIP].y * image_height:
                            pinky_finger_state = 1

                # 손가락 위치 확인한 값을 사용하여 가위,바위,보 중 하나를 출력 해줍니다.
                font = ImageFont.truetype("fonts/gulim.ttc", 80)
                image = Image.fromarray(image)
                draw = ImageDraw.Draw(image)

                text = ""
                if thumb_finger_state == 1 and index_finger_state == 1 and middle_finger_state == 1 and ring_finger_state == 1 and pinky_finger_state == 1:
                    count += 1
                    if(count>20):
                        text = "촬영 시작"
                        shoot=True
                        print("촬영 신호가 감지되었습니다.")
                        count=0
                try:
                    # 소켓통신 부분 , count에 어깨 값 넣어서 보낼 예정
                    if (counts >= 1):
                        last = []
                        # 일단 jump써놓음 향후 어깨 위치 값 보낼 예정
                        UDP_socket.sendto(str(shoulder_points).encode(), (IP, UDP_PORT1))
                        print("손목 y 좌표값 : ", shoulder_points)
                    if (shoot==True):
                        last = []
                        # 일단 jump써놓음 향후 어깨 위치 값 보낼 예정
                        UDP_socket.sendto(("shoot").encode(), (IP, UDP_PORT2))
                        print("촬영신호가 유니티로 전송되었습니다.")
                        shoot=False
                except:
                    print("UDP_Failed!")

                # elif thumb_finger_state == 0 and index_finger_state == 1 and middle_finger_state == 1 and ring_finger_state == 0 and pinky_finger_state == 0:
                #     text = "가위"
                # elif index_finger_state == 0 and middle_finger_state == 0 and ring_finger_state == 0 and pinky_finger_state == 0:
                #     text = "주먹"

                w, h = font.getsize(text)

                x = 50
                y = 50


                draw.rectangle((x, y, x + w, y + h), fill='black')
                draw.text((x, y), text, font=font, fill=(255, 255, 255))
                image = np.array(image)

                # 손가락 뼈대를 그려줍니다.
                mp_drawing.draw_landmarks(
                    image,
                    hand_landmarks,
                    mp_hands.HAND_CONNECTIONS,
                    mp_drawing_styles.get_default_hand_landmarks_style(),
                    mp_drawing_styles.get_default_hand_connections_style())



        cv2.imshow('MediaPipe Hands', image)

        if cv2.waitKey(5) & 0xFF == 27:
            break


# 끝내기
cap.release()
UDP_socket.close()
TCP_socket.close()