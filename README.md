# USB Receipt Printer APPs 

USB Receipt Printer And Raspberry Pi 

# How To Use 

### Download Raspbian on 
https://www.raspberrypi.org/downloads/raspbian/


### 1. Write it to card 
`sudo dd if=<img file> of=/dev/<your card>`


### 2. Enable SSH Server 
Create an empty file named "ssh" on root of your card


### 3. Boot Raspberry Pi and login 
Default: pi/raspberry


### 4. Setup, update and reboot 
`sudo raspi-config`  
`sudo apt update`  
`sudo apt upgrade`  
`sudo reboot`


### 5. Install Python and Pip 
`sudo install python-dev python-usb python-pip libjpeg-dev`


### 6. Install pyusb 
`git clone https://github.com/walac/pyusb.git`  
`python setup.py install`


### 7. Upgrade Pip 
`sudo pip install --upgrade pip`


### 8. Install libs 
`sudo pip install Pillow`  
`sudo pip install image`  
`sudo pip install python-escpos`  
`sudo pip install requests`


### 9. Copy all files in \Raspiberry Pi\ to /home  
Also put font file (Font name in rplib.py).


### 10. Get vendor ID, product Id, in_ep, out_ep 
`lsusb -v`


### 11. Edit rplib.py 
Replace Vendor, Product, In, Out, Dpi and Width.


### 12. Edit rpsync.py 
Replace your sync URL.


### 13. Add crontab jobs 
`crontab -e`  
`*/2 * * * * sudo python /home/rpsync.py`


### 14. Done, Enjoy! 
You can use WindowsAPP and AndroidAPP (Build after replace your own URL)  
Script template in \Raspiberry Pi 
