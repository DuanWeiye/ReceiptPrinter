#!/usr/bin/env python
# -*- coding: UTF-8 -*-

from rplib import *

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

PrinterCut()

