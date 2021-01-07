#!/usr/bin/env bash

# This is a shell script to transform the PROJECTNAME directory into a cookie-cutter template

# Environmental variable options accepted by `generate_template.sh`:

# * `VERBOSE=true`: Prints more verbose output.
# * `SKIP_REGENERATION=true`: Does not alter the generated cookiecutter template.
# * `SKIP_TESTS=true`: Does not perform tests after generating template.
# * `KEEP_COOKIECUTTER_OUTPUT=true`: Do not delete cookiecutter output after running tests (final output is in `ProjectName` directory).
# * `OUTPUT_DIR`: Use a different output directory (default is current directory)


# Delete files that we don't want to include in the template
rm -rf TEMPLATE/PROJECTNAME/DerivedData
rm -rf TEMPLATE/PROJECTNAME/Pods
rm -rf TEMPLATE/PROJECTNAME/Podfile.lock

set -e
set -o pipefail

# Run this script in its own directory
SCRIPT_DIR="$(dirname $0)"
cd $SCRIPT_DIR

echo "Regenerating cookiecutter template from TEMPLATE directory contents..."

#This is the only lookup that is done on filenames
LOOKUP="PROJECTNAME"
EXPANDED="{{ cookiecutter.projectName | replace(' ', '') }}"
LOOKUPDIR="TEMPLATE"
EXPANDEDDIR="{{ cookiecutter.TEMPLATE | replace(' ', '') }}"

if [ ! -z "$OUTPUT_DIR" ] ; then
    echo "Using output directory: $OUTPUT_DIR"
    mkdir $OUTPUT_DIR
    cp -rf "$LOOKUP" "$OUTPUT_DIR/$LOOKUP"
    cp cookiecutter.json "$OUTPUT_DIR/"
    if [ "${SKIP_REGENERATION}" == "true" ] ; then
        cp -rf "$EXPANDED" "$OUTPUT_DIR/$EXPANDED" 
    fi
    cd $OUTPUT_DIR
fi

# Clear out any left over artifacts from last regeneration
if [ "${SKIP_REGENERATION}" != "true" ] ; then
    echo "Deleting old template output..."
    rm -rf "${EXPANDED}/"
    echo "Regenerating template..."
else
    echo "Performing dry run on existing template output..."
fi

# Make the tree
find ./TEMPLATE -type d | while read FILE
do
    NEWFILE1=`echo $FILE | sed -e "s/${LOOKUPDIR}/${EXPANDEDDIR}/g"`
    NEWFILE2=`echo $NEWFILE1 | sed -e "s/${LOOKUP}/${EXPANDED}/g"`
    MKDIR_CMD="mkdir -p \"$NEWFILE2\""
    
    if [ "${VERBOSE}" == "true" ] ; then
        echo "${MKDIR_CMD}"
    fi
    if [ "${SKIP_REGENERATION}" != "true" ] ; then
        eval $MKDIR_CMD
    fi
done

# Copy the files over
find ./TEMPLATE -type f | while read FILE
do
    NEWFILE1=`echo $FILE | sed -e "s/${LOOKUPDIR}/${EXPANDEDDIR}/g"`
    NEWFILE=`echo $NEWFILE1 | sed -e "s/${LOOKUP}/${EXPANDED}/g"`
    COPY_CMD="cp \"$FILE\" \"$NEWFILE\""
    if [ "${VERBOSE}" == "true" ] ; then
        echo "${COPY_CMD}"
    fi
    if [ "${SKIP_REGENERATION}" != "true" ] ; then
        eval $COPY_CMD
    fi
done

# Do replacements
function replace {
    grep -rl $1 ./TEMPLATE | while read FILE
    do 
    NEWFILE1=`echo $FILE | sed -e "s/${LOOKUPDIR}/${EXPANDEDDIR}/g"`
    NEWFILE=`echo $NEWFILE1 | sed -e "s/${LOOKUP}/${EXPANDED}/g"`
        SED_CMD="LC_ALL=C sed -e \"s/$1/$2/g\" \"$NEWFILE\" > t1 && mv t1 \"$NEWFILE\""
        # Copy over incase the sed fails due to encoding
        #echo "echo \"$FILE\""
        if [ "${VERBOSE}" == "true" ] ; then
            echo "${SED_CMD}"
        fi
        if [ "${SKIP_REGENERATION}" != "true" ] ; then
            eval $SED_CMD
        fi        
    done
}

replace "PROJECTNAME" "{{ cookiecutter.projectName | replace(' ', '') }}"
replace "ORGANIZATION" "{{ cookiecutter.companyName }}"
replace "AUTHOR" "{{ cookiecutter.author }}"
replace "DATE" "{{ cookiecutter.date }}"
replace "com.company.PROJECTNAME" "{{ cookiecutter.bundleIdentifier }}"
