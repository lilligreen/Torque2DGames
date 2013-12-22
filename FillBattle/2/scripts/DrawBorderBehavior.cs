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

if (!isObject(DrawBorderBehavior))
{
    %template = new BehaviorTemplate(DrawBorderBehavior);
}

//-----------------------------------------------------------------------------

function DrawBorderBehavior::onBehaviorAdd(%this)
{
    // Get the number of sprites in the main composite
    %count = TileBoard.getSpriteCount();
    %length = mSqrt(%count);

    // Iterate over all the sprites
    for (%x = 0; %x < %length; %x++)
    {
        for (%y = 0; %y < %length; %y++)
        {
            // Add a sprite (aka "tile")
            %this.owner.addSprite(%x SPC %y);
        }
    }

    // Save the grid size for future use
    DrawBorderBehavior.length = %length;
}

//-----------------------------------------------------------------------------

function DrawBorderBehavior::redraw(%this)
{
    // Iterate over all the sprites
    for (%x = 0; %x < DrawBorderBehavior.length; %x++)
    {
        for (%y = 0; %y < DrawBorderBehavior.length; %y++)
        {
            // Check the owner from the equivalent TileBoard tile
            TileBoard.selectSprite(%x SPC %y);
            %dataObject = TileBoard.getSpriteDataObject();

            if (%dataObject.owner == -1)
            {
                %this.owner.selectSprite(%x SPC %y);
                %this.owner.clearSpriteAsset();
                %this.owner.deselectSprite();
                continue;
            }
            
            if (%y < DrawBorderBehavior.length - 1)
            {
                TileBoard.selectSprite(%x SPC %y+1);
                %up = TileBoard.getSpriteDataObject();
            }
            
            if (%x < DrawBorderBehavior.length - 1)
            {
                TileBoard.selectSprite(%x+1 SPC %y);
                %right = TileBoard.getSpriteDataObject();
            }
            
            if (%y > 0)
            {
                TileBoard.selectSprite(%x SPC %y-1);
                %down = TileBoard.getSpriteDataObject();
            }
            
            if (%x > 0)
            {
                TileBoard.selectSprite(%x-1 SPC %y);
                %left = TileBoard.getSpriteDataObject();
            }
            
            %bits = 0;
            %bits |= (%dataObject.owner != %up.owner) << 0;
            %bits |= (%dataObject.owner != %right.owner) << 1;
            %bits |= (%dataObject.owner != %down.owner) << 2;
            %bits |= (%dataObject.owner != %left.owner) << 3;
            
            // Select the correct location again
            %this.owner.selectSprite(%x SPC %y);

            // Assign it an ImageAsset and a random frame
            %this.owner.setSpriteImage("FillBattle:SquareBorders", %bits);
            %this.owner.deselectSprite();
        }
    }
}