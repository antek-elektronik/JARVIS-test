import numpy
file = open("dictionary.txt",'r',encoding="utf-8")
timer = 0
result = ""
arr = numpy.array(file.readlines())

print(arr)