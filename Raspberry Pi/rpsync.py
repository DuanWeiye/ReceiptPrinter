#!/usr/bin/env python
# -*- coding: UTF-8 -*-

#from rplib import *
import requests
from datetime import datetime

req = requests.get('<Put your sync url here>')

reqTime = datetime.now().strftime("%Y/%m/%d %H:%M:%S ")

if req.text.find("NG") == 0:
	print(reqTime + "Sync Failed.")
elif req.text.find("IDLE") == 0:
	print(reqTime+ "Sync Nothing.")
elif req.text.find("OK") == 0:
	print(reqTime + "Sync Successed.")

	from rplib import *

        trimedText = "\n".join(req.text.splitlines())
        textList = trimedText.strip().splitlines()

	isStarted = False	
	for eachLine in textList:
		try:
			print("Debug: Line:" + eachLine.encode('utf-8'))

			if eachLine == "_BEGIN_":
				isStarted = True
				continue
			if not isStarted:
				continue
			if eachLine == "_END_":
				break

			exec(eachLine, globals())

        	except:
			print("Unexpected error:", sys.exc_info()[0])
		
	print("Debug: Completed.")

	printer.close()
else:
	print(reqTime + "Unknown Response:\n" + req.text)
