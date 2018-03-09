#!/bin/bash
#
# Adjust fan speed automatically.
# This version by StuJordan
# Based on Version by DarkPhoinix
# Original script by Mitch Frazier

export DISPLAY=:0

# Target temperature for video card.
target_temp=65

# Value used to calculate the temperature range (+/- target_temp).
target_range=1

# Time to wait before re-checking.
sleep_time=10

# Minimum fan speed.
min_fanspeed=20

# Fan speed increment.
adj_fanspeed=2

if [ "$1" ]; then target_temp=$1; fi

target_temp_low=$(expr $target_temp - $target_range)
target_temp_high=$(expr $target_temp + $target_range)

function chnage_fanspeed {
	nvidia-settings -a [gpu:$1]/GPUFanControlState=1 > /dev/null
	nvidia-settings -a [fan:$1]/GPUTargetFanSpeed=$2 > /dev/null
}

while true
do
	cards_num=`nvidia-smi -L | wc -l`
	for ((i=0; i<$cards_num; i++))
	do
		temp_val=`nvidia-smi -i $i --query-gpu=temperature.gpu --format=csv,noheader`
		pwm_val=`nvidia-smi -i $i --query-gpu=fan.speed --format=csv,noheader | cut -d '%' -f 1 | tr -d '[:space:]'`
		
		echo "Current GPU${i} temp is $temp_val. Current pwm is $pwm_val"

		if [ $temp_val -gt $target_temp_high ]; then
		echo "GPU${i} temperature too high"

		# Temperature above target, see if the fan has any more juice.

		if [ $pwm_val -lt 100 ]; then
			echo "Increasing GPU${i} fan speed, temperature: $temp_val"
			pwm_val=$(expr $pwm_val + $adj_fanspeed)
			if [ $pwm_val -gt 100 ]; then pwm_val=100; fi
			chnage_fanspeed $i $pwm_val 
		fi
		elif [ $temp_val -lt $target_temp_low ]; then
		
			# Temperature below target, lower the fan speed
			# if we're not already at the minimum.
			
			if [ $pwm_val -gt $min_fanspeed ]; then
				echo "Decreasing GPU${i} fan speed, temperature: $temp_val"
				pwm_val=$(expr $pwm_val - $adj_fanspeed)
				if [ $pwm_val -lt $min_fanspeed ]; then pwm_val=$min_fanspeed; fi
				chnage_fanspeed $i $pwm_val
			fi
		fi
	done

	sleep $sleep_time
done