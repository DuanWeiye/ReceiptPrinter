#!/usr/bin/env python
# -*- coding: utf-8 -*-
# Receipt Printer Lib

import os
import sys
import usb.core
import usb.util
import requests

from datetime import datetime
from PIL import Image, ImageFont, ImageOps, ImageDraw
from StringIO import StringIO
from escpos.printer import Usb

# Printer Info
receiptVendor=0x6868
receiptProduct=0x0500

receiptIn=0x84
receiptOut=0x03

receiptDpi=203
receiptWidth=48

# Find Printer
dev = usb.core.find(idVendor=receiptVendor, idProduct=receiptProduct)

# Check Printer
if dev is None:
    raise ValueError('Debug: Receipt Printer Not Found.')
else:
    print ("Debug: Receipt Printer Found.")

# Init Printer
printer = Usb(receiptVendor, receiptProduct, 0, receiptIn, receiptOut)

def PrintAlign(inputPos=0):
	if inputPos == 0:
		printer.set(align='left')
	elif inputPos == 1:
                printer.set(align='center')
	elif inputPos == 2:
                printer.set(align='right')


def PrintCut(inputWithLine=False):
        if inputWithLine:
		PrintSpace(40)
        	PrintLine(1)

        printer.cut()

def PrintTime(inputReservedTime="-"):

	if inputReservedTime == "-":
        	ptTime = datetime.now()
	else:
		ptTime = datetime.strptime(inputReservedTime, "%Y%m%d%H%M%S")

	# Get Datetime
	dateTime = " " + str(ptTime.strftime("%Y/%m/%d %H:%M:%S")) + " "

        # Print Time
        PrintText(dateTime, inputFade=True)


def PrintImage(inputFilePath, inputResizeRatio=85):

	# Get Max Width In Pixel
        maxWidth = int((receiptDpi * receiptWidth) / 25.4)
        #print("Debug: maxWidth(Original): " + str(maxWidth))

	maxWidth = int(maxWidth * inputResizeRatio / 100)

	try:
		if inputFilePath.find("http://") == 0:
			req = requests.get(inputFilePath)
			image = Image.open(StringIO(req.content))
        	else:
			image = Image.open(inputFilePath)

		ratioInHeight = (maxWidth / float(image.size[0]))
		maxHeight = int((float(image.size[1]) * float(ratioInHeight)))
        	image.thumbnail((maxWidth, maxHeight), Image.ANTIALIAS)

		# Print Image
		printer.image(image)

	except IOError:
        	print("Error: Cannot Create Thumbnail")

def PrintSpace(inputHeight=20):
	# Create Image
	image = Image.new('RGB', (1, inputHeight))

        # Draw Image
        draw = ImageDraw.Draw(image)

        # Fade Image
        image = ImageOps.invert(image)

	# Print Image
	printer.image(image)

# Print Lines
def PrintLine(inputLineCount=1, inputLineHeight=4):

	# --- lineHeight ---
        #  <  spaceHeight >

	spaceHeight = 5

	# Get Max Width In Pixel
	maxWidth = int((receiptDpi * receiptWidth) / 25.4)
        #print("Debug: maxWidth(Original): " + str(maxWidth))

	# Create Image
	image = Image.new('RGB', (maxWidth, inputLineCount * (spaceHeight + inputLineHeight)))

        # Draw Image
        draw = ImageDraw.Draw(image)

	# Draw Line
	for eachLine in range(inputLineCount):
		linePos = int((eachLine * (spaceHeight + inputLineHeight)) + inputLineHeight / 2)
        	draw.line(((0, linePos), (maxWidth, linePos)), (255, 255, 255), inputLineHeight)
		#print("Debug: LinePos(" + str(eachLine) + "): " + str(linePos))

	# Fade Image
	image = ImageOps.invert(image)

	# Print Image
	printer.image(image)

# Print Text As Image
def PrintText(inputText, inputSize=24, inputFade=False, inputBold=False, inputBorder=0):
	trimedText = "\n".join(inputText.splitlines())
	textList = trimedText.splitlines()

	#print("Debug: inputText Lines: " + str(len(textList)))

	if inputBold:
		fnt = ImageFont.truetype(os.path.join(os.path.dirname(__file__), 'msyhbd.ttc'), inputSize)
	else:
		fnt = ImageFont.truetype(os.path.join(os.path.dirname(__file__), 'msyh.ttc'), inputSize)

	lineCount = 0
	multiText = []

	# Calculate Maxmium Printable Width In Pixel	
	maxWidth = int((receiptDpi * receiptWidth) / 25.4)
	#print("Debug: maxWidth(Original): " + str(maxWidth))

	# Minus 4x Border Width(2x For Left Side, 2x For Right Side)
	maxWidthFixed = maxWidth - 4 * inputBorder
	#print("Debug: maxWidth(Fixed): " + str(maxWidthFixed))

	# Try Get Full Size Of Text-Image In One Row
	boxFull = fnt.getsize(textList[0])

	# If Overwidth, Cut It Into Multi-Line
	if boxFull[0] > maxWidthFixed or len(textList) > 1:
		#print("Debug: Multi Line.")

		for eachLine in textList:
			lastPos = 0
        		linePos = 0

			while lastPos < len(eachLine):
				linePos += 1
				boxEach = fnt.getsize(eachLine[lastPos:linePos])
				if int(boxEach[0]) < maxWidthFixed and linePos < len(eachLine):
					continue
				else:
					if int(boxEach[0]) > maxWidthFixed:
						linePos -= 1
					lineCount += 1
					multiText.append(eachLine[lastPos:linePos])
					lastPos = linePos

		#print("Debug: Line Count: " + str(lineCount))

		# Create Multi-Line Image
		image = Image.new('RGB', (maxWidth, 4 * inputBorder + lineCount * boxFull[1]))
	else:
		#print("Debug: Single Line")

		multiText.append(textList[0])

		# Create Single-Line Image
		image = Image.new('RGB', (4 * inputBorder + boxFull[0], 4 * inputBorder + boxFull[1]))

	# Draw Image
	draw = ImageDraw.Draw(image)

	# Draw Border
	if inputBorder > 0:
		# Line In Black: Border
		draw.line(((0, int(inputBorder / 2)), (image.size[0], int(inputBorder / 2))), (255, 255, 255), inputBorder)
		draw.line(((image.size[0] - int(inputBorder / 2), int(inputBorder / 2)), (image.size[0] - int(inputBorder / 2), image.size[1] - int(inputBorder / 2))), (255, 255, 255), inputBorder)
		draw.line(((image.size[0], image.size[1] - int(inputBorder / 2)), (0, image.size[1] - int(inputBorder / 2))), (255, 255, 255), inputBorder)
		draw.line(((int(inputBorder / 2), image.size[1] - int(inputBorder / 2)), (int(inputBorder / 2), int(inputBorder / 2))), (255, 255, 255), inputBorder)

	# Draw Text
	currentLine = 0
	for eachLine in multiText:
		#print("Debug: PrintText [" + eachLine + "]")
    		draw.text((2 * inputBorder, 2 * inputBorder + currentLine * boxFull[1]), eachLine, font=fnt)
    		currentLine += 1

	# Fade Image If Needed
	if not inputFade:
		image = ImageOps.invert(image)

	# Print Image
	printer.image(image)

if __name__ == '__main__':

	# Some Text
	ptTitle = u'Test Title'
	ptText = u'Test Text'
	ptQR = u'This is my Alipay QRCode'

	# Print Something
	PrintAlign(1)
	PrintTime()
	PrintText(ptTitle, inputSize=36, inputBold=True)
	PrintSpace(20)

	PrintAlign(0)
	PrintText(ptText, inputBorder=2)
	PrintSpace(20)

	PrintAlign(1)
	PrintText(ptQR, inputSize=20)
	PrintImage("http://brainexplode.com/money.jpg")
	#printer.qr('http://brainexplode.com/me.html', ec=2, size=8)

	PrintSpace(20)
	PrintLine(1)

	printer.cut()
	printer.close()
