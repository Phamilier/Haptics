# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.


from gdx import gdx
import time
import zmq
import struct

context = zmq.Context()

#wait_for_start_signal()

socket = context.socket(zmq.PUSH)
socket.setsockopt(zmq.SNDHWM, 50)
socket.bind("tcp://*:5555")

gdx = gdx.gdx()


gdx.open(connection='usb')
gdx.select_sensors([1])
gdx.start(10)

column_headers= gdx.enabled_sensor_info()   # returns a string with sensor description and units
#print('\n')
#print(column_headers)

while True:

    measurements = gdx.read()
    if measurements == None:
        break
    print(measurements[0])

    measurement_bytes = struct.pack('f', measurements[0])

    time.sleep(0.005)

    socket.send(measurement_bytes)

gdx.stop()
gdx.close()
# See PyCharm help at https://www.jetbrains.com/help/pycharm/
