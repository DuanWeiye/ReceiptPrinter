# USB Receipt Printer APPs 

USB Receipt Printer And Raspberry Pi 

# How To Use 

### 1. Download Raspbian on 
https://www.raspberrypi.org/downloads/raspbian/


### 2. Write it to card 
`sudo dd if=<img file> of=/dev/<your card>`


### 3. Enable SSH Server 
Create an empty file named "ssh" on root of your card


### 4. Boot Raspberry Pi and login 
Default: pi/raspberry


### 5. Setup, update and reboot 
`sudo raspi-config`  
`sudo apt update`  
`sudo apt upgrade`  
`sudo reboot`


### 6. Install Python and Pip 
`sudo install python-dev python-usb python-pip libjpeg-dev`


### 7. Install pyusb 
`git clone https://github.com/walac/pyusb.git`  
`python setup.py install`


### 8. Upgrade Pip 
`sudo pip install --upgrade pip`


### 9. Install libs 
`sudo pip install Pillow`  
`sudo pip install image`  
`sudo pip install python-escpos`  
`sudo pip install requests`


### 10. Copy all files in \Raspiberry Pi\ to /home  
Also put font file (Font name in rplib.py).


### 11. Get vendor ID, product Id, in_ep, out_ep 
`lsusb -v`


### 12. Edit rplib.py 
Replace Vendor, Product, In, Out, Dpi and Width.


### 13. Edit rpsync.py 
Replace your sync URL.


### 14. Add crontab jobs 
`crontab -e`  
`*/2 * * * * sudo python /home/rpsync.py`


### 15. Done, Enjoy! 
You can use WindowsAPP and AndroidAPP (Build after replace your own URL)  
Script template in \Raspiberry Pi 
