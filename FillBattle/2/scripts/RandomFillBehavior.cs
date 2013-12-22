//-----------------------------------------------------------------------------
// Copyright (c) 2013 Mike Lilligreen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

if (!isObject(RandomFillBehavior))
{
    %template = new BehaviorTemplate(RandomFillBehavior);

    %template.addBehaviorField(GridSize, "The size of the board", int, 15);
}

//-----------------------------------------------------------------------------

function RandomFillBehavior::onBehaviorAdd(%this)
{
    // Iterate over all the sprites
    for (%x = 0; %x < %this.GridSize; %x++)
    {
        for (%y = 0; %y < %this.GridSize; %y++)
        {
            // Pick a random number from 0 to 5
            // This will equal the frame number from the ImageAsset
            %randomFrame = getRandom(0,5);

            // Add a sprite (aka "tile")
            %this.owner.addSprite(%x SPC %y);

            // Assign it an ImageAsset and a random frame
            %this.owner.setSpriteImage("FillBattle:SixColors", %randomFrame);

            // Create a data object
            %dataObject = new ScriptObject();

            // Track the owner. Initialize with -1 (no owner)
            %dataObject.owner = -1;

            // Track tile visits. Initialize with false
            %dataObject.visited = false;

            // Assign the tile its data object
            %this.owner.setSpriteDataObject(%dataObject);
        }
    }
}