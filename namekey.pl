#! perl
# File:     namekey.pl
# Created:  March 28, 2000
# By:       Eric Hasz
# Purpose:  Filter the keywords out of the INR files in the customer support
#       examples.  The output HTML file will have each of the keywords followed
#       by all of the INR files that contain that keyword.
# Usage:    Run from the directory where the INR files reside.  There is a file
#	called <command_macro.txt> that is used by this script.  It needs to be in
#	the directory with the INR files.  The perl script should be called like 
#	this: 	perl namekey.pl -f *.inr -o myout
#       The output will be <example_scripts.html> if myout is not specified.
#
# Modifications:
#		2001.01.05 EPH
#		Now sorting by Category.

### set up arguments -f and -o ###
use strict;
use vars qw($opt_f $opt_o);

use Getopt::Std ();

undef $opt_f;
undef $opt_o;

Getopt::Std::getopt('foh');
my(@filelist) = glob $opt_f;
if (!defined $opt_f)
{
    print "\nusage: namekey.pl -f <filename(s)> [-o <output file>]\n";
    exit 1;
}
my $outfile = defined $opt_o ? $opt_o : "example_scripts.html";


### Load IxHash for hash table usage ###
use Tie::IxHash;
my %inrNames = ();
my %AllKeywords = ();
my %Command = ();
my %Primer = ();
my %Concept = ();
tie %inrNames, "Tie::IxHash";
tie %AllKeywords, "Tie::IxHash";
tie %Command, "Tie::IxHash";
tie %Primer, "Tie::IxHash";
tie %Concept, "Tie::IxHash";
my $name = "";
my $count = 0;
my $time = localtime;

### Fill %Command with all ASAP commands ###
my $commandFile = 'command_macro.txt';
open (COMMAND, "<$commandFile") || die "cannot open $commandFile\n";
my(@commands) = <COMMAND>;
close (COMMAND);
foreach my $command (@commands)
{
    $command =~ s/\n*\s*\t*$//;
    $AllKeywords{$command} = '';
}

### Step through all requested files in the file list. ###
foreach my $file (@filelist)
{
    my(@filename) = split(/[\n]/, $file);

    ### Step through each line of the current INR file. ###
    foreach my $inrFile (@filename)
    {
        open(INRFILE, "<$inrFile") || die "cannot open $inrFile\n";
        my(@openInr) = <INRFILE>;
        close(INRFILE);

        ### Process the header of the INR file ###
        foreach my $line (@openInr)
        {
            if ($line =~ m/!!\+\+/)  ### start of header for INR files
            {
                next;
            }
            elsif ($line =~ m/!! (.*).INR/)
            {
                $name = $1.".INR";
            }
            elsif ($line =~ m/Title: /)
            {
                my $title = $line;
                $title =~ s/\n*\s*\t*$//;
                $title =~ s/!! Title: //;
                $title = "$title<A HREF=\"" . $name . "\">\"$title\"</A>";
                $name = $title;
            }
            elsif ($line =~ m/Category: /)
            {
				my $category = $line;
				$category =~ s/\n*\s*\t*$//;
				$category =~ s/!! Category: //;
				$name = $category.": ".$name;
			}
            elsif ($line =~ m/Keywords: /)
            {
                my $words = $line;
                $words =~ s/\n*\s*\t*$//;
                $words =~ s/!! Keywords: //;
                $inrNames{$name} .= $words;
            }
            elsif ($line =~ m/!!\-\-/)  ### end of header for INR files
            {
                last;
            }
            else {next};

         }
    }

}

### Each INR is in the hash %inrNames in the form (X.INR:keyword1,keyword2,keyword3...) ###
### This will put each keyword into one of the (Command, Primer, Concept) hashes with   ###
### the INR filenames as the values.                                                    ###
foreach my $names (sort(keys %inrNames))
{
    my @keys = split(/, /, $inrNames{$names});
    for my $key (sort (@keys))
    {
        #print "$key:";
        $AllKeywords{$key} .= "<P><DIR>".$names."</P></DIR>\n";
        if ($key =~ m/[0-9,A-Z]{2,}/)  ### The keyword is an ASAP command. ###
        {
            $Command{$key} .= "<P><DIR>".$names."</P></DIR>\n";
        }
        elsif ($key =~ m/[A-Z]{1}/)    ### The keyword is an ASAP primer chapter. ###
        {
            #print "$key\n";
            $Primer{$key}  .= "<P><DIR>".$names."</P></DIR>\n";
        }
        else                           ### The keyword is an ASAP concept. ###
        {
            $Concept{$key} .= "<P><DIR>".$names."</P></DIR>\n";
        }
    }
}

open(OUTFILE, ">$outfile") || die "cannot open $outfile\n";

print OUTFILE '
<HTML>
<HEAD>
<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=windows-1252">
<TITLE>Customer Service - Example Scripts</TITLE>
</HEAD>
<BODY TEXT="#000000" LINK="#0000ff" VLINK="#800080" BGCOLOR="#ffffff">

</FONT><H2>Customer Service - Example Scripts<A NAME="examples"></A></H2>

<P>This page contains an alphabetical listing of all of the keywords used 
within the example scripts.  There are links to each of the INR files based on the title 
of the script. Search for specific keyword, sorted alphabetically.</P>',
"<P ALIGN=\"RIGHT\">Generated on $time",
'<P>
<A HREF="#A">A</A> | <A HREF="#B">B</A> | <A HREF="#C">C</A> | <A HREF="#D">D</A> | 
<A HREF="#E">E</A> | <A HREF="#F">F</A> | <A HREF="#G">G</A> | <A HREF="#H">H</A> | 
<A HREF="#I">I</I> | <A HREF="#J">J</A> | <A HREF="#K">K</A> | <A HREF="#L">L</A> | 
<A HREF="#M">M</A> | <A HREF="#N">N</A> | <A HREF="#O">O</A> | <A HREF="#P">P</A> | 
<A HREF="#Q">Q</A> | <A HREF="#R">R</A> | <A HREF="#S">S</A> | <A HREF="#T">T</A> | 
<A HREF="#U">U</A> | <A HREF="#V">V</A> | <A HREF="#W">W</A> | <A HREF="#X">X</A> | 
<A HREF="#Y">Y</A> | <A HREF="#Z">Z</A> | <A HREF="#2">2</A> | <A HREF="#3">3</A>
</P>
';

### Add all keywords to output file. ###
my @alphabet = split(/ */,"ABCDEFGHIJKLMNOPQRSTUVWXYZ23");
for my $alpha (@alphabet)
{
    print OUTFILE "<B><P><A NAME=\"$alpha\">$alpha</A></B></P><DIR>\n";
    $count = 0;
    for my $key (sort {uc($a) cmp uc($b)} (keys %AllKeywords))
    {
        if (($key =~ m/\A$alpha/i) | ($key =~ m/\A\$$alpha/i))
        {
            $count++;
            if ($count==1)
            {
                print OUTFILE '<A HREF="#', $key, '">', $key, '</A>';
            }
            else
            {
                print OUTFILE ' | <A HREF="#', $key, '">', $key, '</A>';
            }
        }
    }
    print OUTFILE '<P ALIGN="CENTER"><A HREF="#TOP">Top of page.</A></P></DIR><P></P>';
}
print OUTFILE "\n-----------\n";

### Add INR files and Titles to output file. ###
for my $key (sort {uc($a) cmp uc($b)} (keys %AllKeywords))
{
	my $title = $AllKeywords{$key};
	$title =~ s/Isolated Command: (.*)<A HREF/Isolated Commands<\/P><\/DIR>\n<P><DIR><A HREF/;
	$title =~ s/Simple Problem: (.*)<A HREF/Simple Problems<\/P><\/DIR>\n<P><DIR><A HREF/;
	$title =~ s/Isolated Command: (.*)<A HREF/<A HREF/g;
	$title =~ s/Simple Problem: (.*)<A HREF/<A HREF/g;
    print OUTFILE '<B><P><A NAME="', $key, '">', $key, "</A></P></B>\n",
		"<P><DIR>", $title, "</P>",
        "<P ALIGN=\"CENTER\"><A HREF=\"#TOP\">Top of page.</A></P></DIR>\n";
}

close(OUTFILE);

exit 1;
